using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    public class CustomMaterial : BaseMaterial
    {
        double _AmbientCoeff = 0;
        double _ReflectionCoeff = 0;
        double _DiffuseCoeff = 0;
        double _SpecularCoeff = 0;

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
            TransparentColor = new Color();
            SpecularColor = new Color();
            ReflectiveColor = new Color();
        }
    }
}
