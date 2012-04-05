using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Shaders
{
    public class PhongShader:Shader
    {
        public override Color GetColor(Primitive HitObject, LightSource Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal, Color AmbientColor)
        {
            var result = new Color();
            result += new DiffuseShader().GetColor(HitObject, Light, ViewDirection, LightDirection, Normal, AmbientColor);
            result += new SpecularShader().GetColor(HitObject, Light, ViewDirection, LightDirection, Normal, AmbientColor);
            result += AmbientColor;
            return result;
        }
    }
}
