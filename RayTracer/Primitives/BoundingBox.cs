using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Primitives
{
    public class BoundingBox : Primitive
    {
        private Vector3D _maxVector;
        private Vector3D _minVector;

        public BoundingBox(Vector3D MaxVector, Vector3D MinVector)
        {
            _maxVector = MaxVector;
            _minVector = MinVector;
        }


        public override Collision Intersection(Ray ray)
        {
            bool fRet = false;

            double ox = ray.Origin.x;
            double oy = ray.Origin.y;
            double oz = ray.Origin.z;
            double dx = ray.Direction.x;
            double dy = ray.Direction.y;
            double dz = ray.Direction.z;

            double tx_min, ty_min, tz_min;
            double tx_max, ty_max, tz_max;

            double a = 1.0 / dx;

            if (a >= 0)
            {
                tx_min = (_minVector.x - ox) * a;
                tx_max = (_maxVector.x - ox) * a;
            }
            else
            {
                tx_min = (_maxVector.x - ox) * a;
                tx_max = (_minVector.x - ox) * a;
            }

            double b = 1.0 / dy;
            if (b >= 0)
            {
                ty_min = (_minVector.y - oy) * b;
                ty_max = (_maxVector.y - oy) * b;
            }
            else
            {
                ty_min = (_maxVector.y - oy) * b;
                ty_max = (_minVector.y - oy) * b;
            }

            double c = 1.0 / dz;
            if (c >= 0)
            {
                tz_min = (_minVector.z - oz) * c;
                tz_max = (_maxVector.z - oz) * c;
            }
            else
            {
                tz_min = (_maxVector.z - oz) * c;
                tz_max = (_minVector.z - oz) * c;
            }

            double t0, t1;

            t0 = Globals.Max(tx_min, ty_min);
            t0 = Globals.Max(t0, tz_min);

            t1 = Globals.Min(tx_max, ty_max);
            t1 = Globals.Min(t1, tz_max);

            fRet = t0 < t1 && t1 > Globals.epsilon;

            if (fRet)
                return new Collision(true);
            else
                return new Collision(false);
        }

        public override Vector3D GetNormal(Vector3D point)
        {
            return null;
        }
    }
}
