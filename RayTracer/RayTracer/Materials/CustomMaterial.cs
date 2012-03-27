using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    public class CustomMaterial : Materials.BaseMaterial
    {
        private double _ReflectionCoeff, _RefractionCoeff, _LambertCoeff;

        public override double ReflectionCoeff
        {
            get { return _ReflectionCoeff; }
        }

        public override double RefractionCoeff
        {
            get { return _RefractionCoeff; }
        }

        public override double LambertCoeff
        {
            get { return _LambertCoeff; }
        }

        public CustomMaterial( Color color, double reflectionCoeff, double refractionCoeff, double lambertCoeff)
        {
            _ReflectionCoeff = reflectionCoeff;
            _RefractionCoeff = refractionCoeff;
            _LambertCoeff = lambertCoeff;
            Color = color;
        }
    }
}
