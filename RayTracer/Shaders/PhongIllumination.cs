using RayTracer.Lights;
using RayTracer.Primitives;

namespace RayTracer.Shaders
{
    public class PhongIllumination : IShader
    {
        public Color GetColor(Primitive HitObject, Light Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            var m = HitObject.Material;
            var a = Light.Attenuation.x;
            var b = Light.Attenuation.y;
            var c = Light.Attenuation.z;
            var d = LightDirection.Distance();
            var att = 1 / (a + b * d + c * d * d);

            var reflectedLight = Normal * LightDirection;
            if (reflectedLight < 0)
                return HitObject.Material.DiffuseColor;

            //get diffused color
            var result = HitObject.Material.DiffuseColor * reflectedLight * Light.Color * att;

            //get specular
            result += new SpecularShader().GetColor(HitObject, Light, ViewDirection, LightDirection, Normal);

            return result;
        }
    }
}
