using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Materials
{
    class LambertianMaterial: BaseMaterial
    {
        public override bool IsReflective
        {
            get { return false; }
        }

        public override double ReflectionCoeff
        {
            get { return 0; }
        }

        public override double LambertCoeff
        {
            get { return 1.0; }
        }
    }
}
