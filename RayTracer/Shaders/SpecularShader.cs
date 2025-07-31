using System;
using RayTracer.Lights;
using RayTracer.Primitives;

namespace RayTracer.Shaders
{
    public class SpecularShader : IShader
    {
        public Color GetColor(Primitive HitObject, Light Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            var result = new Color();
            var L = LightDirection.Normalize();
            var V = ViewDirection.Normalize();
            var N = Normal.Normalize();

            var R = L - 2 * (L * N) * N;
            var dot = V * R;
            if (dot > 0)
            {
                var spec = Math.Pow(dot, HitObject.Material.Exponent) * HitObject.Material.SpecularCoeff * HitObject.Material.SpecularColor;
                result += spec * Light.Color;
            }

            return result;
        }
    }
}
