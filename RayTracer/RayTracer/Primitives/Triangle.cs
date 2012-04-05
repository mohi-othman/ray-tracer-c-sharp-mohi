using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Objects
{
    public class Triangle : Primitive
    {
        public Vector3D Point1 { get; set; }
        public Vector3D Point2 { get; set; }
        public Vector3D Point3 { get; set; }
        private Vector3D _normal;

        public Triangle(Vector3D point1, Vector3D point2, Vector3D point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;

            var b = Point3 - Point1;
            var c = Point2 - Point1;
            var normal = Vector3D.Cross(c, b);

            _normal = normal.Normalize();
        }

        public override Collision Intersection(Ray ray)
        {

            var d = ray.Direction * _normal;

            if (d != 0)
            {
                var t = (Point1 - ray.Origin) * _normal / d;
                if (t > Globals.epsilon)
                {
                    var x = ray.Origin + ray.Direction * t;
                    if (Vector3D.Cross((Point2 - Point1), (x - Point1)) * _normal > 0 &&
                        Vector3D.Cross((Point3 - Point2), (x - Point2)) * _normal > 0 &&
                        Vector3D.Cross((Point1 - Point3), (x - Point3)) * _normal > 0)
                    {
                        var hitPoint = ray.Origin + (t * ray.Direction);
                        return new Collision(true, false, hitPoint, this, _normal, t);
                    }
                }

            }
            return new Collision();

        }

        public override Vector3D GetNormal(Vector3D point)
        {
            return _normal;
        }
    }
}
