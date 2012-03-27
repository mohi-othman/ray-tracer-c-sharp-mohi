using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Objects
{
    public class Rectangle: SolidObject
    {
        public Vector3D P1 { get; set; }
        public Vector3D P2 { get; set; }
        public Vector3D P3 { get; set; }
        public Vector3D P4 { get; set; }

        public override double Intersection(Ray ray)
        {
            var N = GetNormal(null);
            var t = N * (P1 - ray.Origin) / (ray.Origin * ray.Direction);

            if (t > 0)
            {
                var p = ray.Origin + t * ray.Direction;
                return NoColision;
            }
            else
            {
                return NoColision;
            }            
        }

        public override Vector3D GetNormal(Vector3D point)
        {
            return (P3 - P2).Multiply((P1 - P2));
        }
    }
}
