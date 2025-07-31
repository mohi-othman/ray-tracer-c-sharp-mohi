using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Lights
{
    public class PointLight : Light
    {
        public PointLight(Vector3D attenuation)
        {
            Attenuation = attenuation;
        }

        public override Vector3D GetLightDirection(Vector3D targetPoint)
        {
            return (Location - targetPoint).Normalize();
        }
    }
}
