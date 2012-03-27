using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Shaders
{
    public class SpecularShader:Shader
    {
        public override Color GetColor(SolidObject HitObject, LightSource Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            var c1 = -(LightDirection * Normal);
            var reflectionDirection = LightDirection + (2 * Normal * c1);

            var dot = ViewDirection * reflectionDirection;

            if (dot > 0)
            {
                var spec = Math.Pow(dot, .1) * 0.2;
                return spec * HitObject.Material.Color;
            }
            else
            {
                return new Color();
            }
        }
    }
}
