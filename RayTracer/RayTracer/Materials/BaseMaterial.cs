using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    public abstract class BaseMaterial
    {
        public abstract bool IsReflective { get; }
        public abstract double ReflectionCoeff { get; }
        public abstract double LambertCoeff { get; }
    }
}
