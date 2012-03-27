using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    public abstract class BaseMaterial
    {        
        public abstract double ReflectionCoeff { get; }
        public abstract double RefractionCoeff { get; }
        public abstract double LambertCoeff { get; }

        public Color Color { get; set; }
    }
}
