using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    public abstract class BaseMaterial
    {
        public abstract double AmbientCoeff { get; }
        public abstract double ReflectionCoeff { get; }
        public abstract double DiffuseCoeff { get; }
        public abstract double SpecularCoeff { get; }

        public Color DiffuseColor { get; set; }
        public Color SpecularColor { get; set; }
        public Double Exponent { get; set; }
        public Color TransparentColor { get; set; }
        public Color ReflectiveColor { get; set; }
        public double RefractionCoeff { get; set; }
        public double RefractionIndex { get; set; }
    }
}
