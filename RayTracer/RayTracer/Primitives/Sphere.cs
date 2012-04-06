﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Objects
{
    class Sphere : Primitive
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
            var B = 2 * (ray.Origin - this.Location) * ray.Direction;
            var C = (ray.Origin - this.Location) * (ray.Origin - this.Location) - Radius * Radius;

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

            //var A = ray.Direction * ray.Direction;
            //var B = 2 * (ray.Origin - this.Location) * ray.Direction;
            //var C = (ray.Origin - this.Location) * (ray.Origin - this.Location) - Radius * Radius;

            //var disc = B * B - 4 * A * C;

            //if (disc < 0)
            //    return new Collision();

            //var distSqrt = Math.Sqrt(disc);


            //var t0 = q / A;
            //var t1 = C / q;

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
                return new Collision(true, true, this, t1);
            }

            return new Collision(true, false, this, t0);

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
