using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Objects
{
    public class Plane : Primitive
    {
        public double Distance { get; set; }
        public Vector3D Normal { get; set; }

        public override Collision Intersection(Ray ray)
        {
            var d = Normal.Normalize() * ray.Direction.Normalize();
            if (d != 0)
            {
                var dist = -((Normal.Normalize() * ray.Origin) + Distance) / d;
                if (dist > 0)
                {
                    var hitPoint = ray.Origin + (dist * ray.Direction.Normalize());
                    return new Collision(true, false, this, dist);
                }
            }
            return new Collision(false);
        }

        public override Vector3D GetNormal(Vector3D point)
        {
            return Normal.Normalize();
        }

        public Plane(double distance, Vector3D normal)
        {
            Distance = distance;
            Normal = normal;
        }

    }
}
