using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Lights
{
    public class PointLight:LightSource
    {
        public PointLight()
        {
            Attenuation = new Vector3D(0.5, 0.5, 0.5);
        }
    }
}
