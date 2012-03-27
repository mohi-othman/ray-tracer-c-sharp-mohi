using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public abstract class LightSource
    {
        public Vector3D Location { get; set; }

        public Color Color { get; set; }

        public double Intensity { get; set; }
    }
}
