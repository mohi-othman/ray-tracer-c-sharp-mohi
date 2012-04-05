using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    public class CustomMaterial : BaseMaterial
    {
        double _AmbientCoeff;
        double _ReflectionCoeff;
        double _DiffuseCoeff;
        double _SpecularCoeff;

        public override double AmbientCoeff
        {
            get { return _AmbientCoeff; }
        }

        public override double ReflectionCoeff
        {
            get { return _ReflectionCoeff; }
        }

        public override double DiffuseCoeff
        {
            get { return _DiffuseCoeff; }
        }

        public override double SpecularCoeff
        {
            get { return _SpecularCoeff; }
        }

        public CustomMaterial(double ambientCoeff, double reflectionCoeff, double diffuseCoeff, double specularCoeff)
        {
            _AmbientCoeff = ambientCoeff;
            _ReflectionCoeff = reflectionCoeff;
            _DiffuseCoeff = diffuseCoeff;
            _SpecularCoeff = specularCoeff;
        }
    }
}
