using System;

namespace RayTracer.Primitives
{
    public class Sphere : Primitive
    {
        public double Radius { get; set; }

        public Sphere(Vector3D Center, double size)
        {
            Location = Center;
            Radius = size;
        }

        public override Collision Intersection(Ray ray)
        {
            var A = ray.Direction * ray.Direction;
            var B = 2 * (ray.Origin - Location) * ray.Direction;
            var C = (ray.Origin - Location) * (ray.Origin - Location) - Radius * Radius;

            var d = B * B - 4 * A * C;
            if (d < 0)
                return new Collision(false);

            var sqrtD = Math.Sqrt(d);

            double q;
            if (B < 0)
                q = (-B - sqrtD) / 2;
            else
                q = (-B + sqrtD) / 2;

            var t0 = q / A;
            var t1 = C / q;

            if (t0 > t1)
            {
                var temp = t0;
                t1 = t0;
                t0 = temp;
            }

            if (t1 < 0)
                return new Collision(false);

            if (t0 < 0)
            {
                //Origin inside sphere                
                var hitPoint1 = ray.Origin + ray.Direction * t1;
                return new Collision(true, true, this, t1, GetNormal(hitPoint1), hitPoint1);
            }

            var hitPoint0 = ray.Origin + ray.Direction * t0;
            return new Collision(true, false, this, t0, GetNormal(hitPoint0), hitPoint0);

        }


        public override Vector3D GetNormal(Vector3D point)
        {
            var n = point - Location;
            var temp = n * n;
            temp = 1 / Math.Sqrt(temp);
            n = temp * n;

            return n;
        }
    }
}
