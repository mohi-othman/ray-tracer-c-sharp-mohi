using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Primitives
{
    public class Triangle : Primitive
    {
        public Vector3D Point0 { get; set; }
        public Vector3D Point1 { get; set; }
        public Vector3D Point2 { get; set; }
        private Vector3D _u, _v, _normal;

        public Triangle(Vector3D point0, Vector3D point1, Vector3D point2)
        {
            Point0 = point0;
            Point1 = point1;
            Point2 = point2;

            // get triangle edge vectors and plane normal            
            _u = Point1 - Point0;
            _v = Point2 - Point0;
            _normal = Vector3D.Cross(_u, _v);   // cross product

        }

        public override Collision Intersection(Ray ray)
        {
            //http://softsurfer.com/Archive/algorithm_0105/algorithm_0105.htm#intersect_RayTriangle()            
            Vector3D dir, w0, w;          // ray vectors
            double r, a, b;             // params to calc ray-plane intersect


            dir = ray.Direction;             // ray direction vector
            w0 = ray.Origin - Point0;
            a = -(_normal * w0);
            b = _normal * dir;
            if (Math.Abs(b) < Globals.epsilon)
            {     // ray is parallel to triangle plane
                return new Collision(false);
            }

            // get intersect point of ray with triangle plane
            r = a / b;
            if (r < 0.0)                        // ray goes away from triangle
                return new Collision(false);    // => no intersect

            // for a segment, also test if (r > 1.0) => no intersect

            var hitPoint = ray.Origin + r * dir;           // intersect point of ray and plane

            // is I inside T?
            double uu, uv, vv, wu, wv, D;

            uu = _u * _u;
            uv = _u * _v;
            vv = _v * _v;
            w = hitPoint - Point0;
            wu = w * _u;
            wv = w * _v;
            D = uv * uv - uu * vv;

            // get and test parametric coords
            double s, t;
            s = (uv * wv - vv * wu) / D;
            if (s < 0.0 || s > 1.0)        // I is outside T
                return new Collision(false);
            t = (uv * wu - uu * wv) / D;
            if (t < 0.0 || s + t > 1.0)  // I is outside T
                return new Collision(false);

            var dist = Vector3D.Distance(ray.Origin, hitPoint);

            return new Collision(true, false, this, dist, _normal, hitPoint);                     // I is in T
        }

        public override Vector3D GetNormal(Vector3D point)
        {
            return _normal;
        }
    }
}
