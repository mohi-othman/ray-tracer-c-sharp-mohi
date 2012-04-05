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

        public override double Intersection(Ray ray)
        {
            var d = Normal * ray.Direction;
            if (d != 0)
            {
                var dist = -((Normal * ray.Origin) + Distance) / d;
                if (dist > Globals.epsilon)
                {
                    return dist;
                }
            }
            return NoColision;
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
