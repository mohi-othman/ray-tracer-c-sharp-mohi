using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public abstract class Light
    {
        public Vector3D Location { get; set; }

        public Color Color { get; set; }

        public Vector3D Attenuation { get; set; }

        public abstract Vector3D GetLightDirection(Vector3D targetPoint);
    }
}
