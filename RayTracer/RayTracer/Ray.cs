using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class Ray
    {
        public Vector3D Origin { get; set; }
        public Vector3D Direction { get; set; }

        public Ray(Vector3D origin, Vector3D direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
