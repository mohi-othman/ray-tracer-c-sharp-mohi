using RayTracer.Lights;
using RayTracer.Primitives;

namespace RayTracer.Shaders
{
    public class DiffuseShader : IShader
    {
        public Color GetColor(Primitive HitObject, Light Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            var L = LightDirection.Normalize();
            if (HitObject.Material.DiffuseCoeff > 0)
            {
                var dot = L * Normal;
                if (dot > 0)
                {
                    var diff = dot * HitObject.Material.DiffuseCoeff;
                    return diff * HitObject.Material.DiffuseColor * Light.Color;
                }
            }

            return new Color();
        }
    }
}
