using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    public class PhongMaterial : Material
    {
        public override double AmbientCoeff
        {
            get { return .2; }
        }

        public override double ReflectionCoeff
        {
            get { return 0; }
        }

        public override double DiffuseCoeff
        {
            get { return 1; }
        }

        public override double SpecularCoeff
        {
            get { return .01; }
        }

        public PhongMaterial(Color diffuseColor, Color specularColor, double exponent, Color transparentColor, Color relexiveColor, double refractionIndex)
        {
            DiffuseColor = diffuseColor;
            SpecularColor = specularColor;
            Exponent = 100;
            TransparentColor = transparentColor;
            ReflectiveColor = relexiveColor;
            RefractionIndex = refractionIndex;
        }
    }
}
