using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Materials
{
    public class CustomMaterial : Material
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
            TransparentColor = new Color(1, 1, 1);
            SpecularColor = new Color(1, 1, 1);
            ReflectiveColor = new Color(0, 0, 0);
            Exponent = 20;
            RefractionIndex = 1.5;
            RefractionCoeff = 0;
        }
    }
}
