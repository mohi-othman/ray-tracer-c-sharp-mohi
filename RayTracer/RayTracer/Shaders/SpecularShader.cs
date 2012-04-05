using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Shaders
{
    public class SpecularShader:Shader
    {
        public override Color GetColor(Primitive HitObject, LightSource Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal, Color AmbientColor)
        {
            var result = new Color();
            var L = LightDirection.Normalize();
            var V = ViewDirection.Normalize();
            var N = Normal.Normalize();

            var R = L - 2 * (L * N) * N;
            var dot = V * R;
            if (dot > Globals.epsilon)
            {
                var spec = Math.Pow(dot, HitObject.Material.Exponent) * HitObject.Material.SpecularColor;
                result += spec * Light.Color;
            }
            
            return result;
        }
    }
}
