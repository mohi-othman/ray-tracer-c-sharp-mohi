using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Shaders
{
    public class PhongIllumination : Shader
    {
        public override Color GetColor(Primitive HitObject, LightSource Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal, Color AmbientColor)
        {
            var m = HitObject.Material;
            var a = Light.Attenuation.x;
            var b = Light.Attenuation.y;
            var c = Light.Attenuation.z;
            var d = LightDirection.Distance();
            var att = 1 / (1 + b * d + c * d * d);

            var reflection = (2 * Normal * (Normal * LightDirection) - LightDirection).Normalize();

            var result = AmbientColor * m.AmbientCoeff * m.DiffuseCoeff + att * Light.Color * (m.DiffuseCoeff * m.DiffuseColor * (Normal * LightDirection) + m.SpecularCoeff * m.SpecularColor * Math.Pow(reflection * ViewDirection, m.Exponent));

            return result;
        }
    }
}
