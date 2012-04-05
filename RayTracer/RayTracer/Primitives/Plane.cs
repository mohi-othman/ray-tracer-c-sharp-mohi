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
            var d = Normal * ray.Direction;
            if (d != 0)
            {
                var dist = -((Normal * ray.Origin) + Distance) / d;
                if (dist > Globals.epsilon)
                {
                    var hitPoint = ray.Origin + (dist * ray.Direction);
                    return new Collision(true, false, hitPoint, this, Normal, dist);
                }
            }
            return new Collision();
        }

        public override Vector3D GetNormal(Vector3D point)
        {
            return Normal;
        }

        public Plane(double distance, Vector3D normal)
        {
            Distance = distance;
            Normal = normal;
        }

    }
}
