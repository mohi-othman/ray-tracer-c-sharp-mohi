using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Shaders
{
    public class DiffuseShader : Shader
    {
        public override Color GetColor(SolidObject HitObject, LightSource Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            var dot = Normal * LightDirection;
            if (dot > 0)
            {
                var diffuse = dot * HitObject.Material.LambertCoeff;
                return diffuse * HitObject.Material.Color * Light.Color;
            }
            else
            {
                return new Color();
            }
        }

        

    }
}
