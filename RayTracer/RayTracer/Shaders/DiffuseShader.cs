using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Shaders
{
    public class DiffuseShader : Shader
    {
        public override Color GetColor(Primitive HitObject, LightSource Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal, Color AmbientColor)
        {
            var L = LightDirection;
            if (HitObject.Material.DiffuseCoeff > 0)
            {
                var dot = Normal * L;
                if (dot > Globals.epsilon)
                {
                    var diff = dot * HitObject.Material.DiffuseCoeff;
                    return diff * HitObject.Material.DiffuseColor * Light.Color;
                }
            }
            return new Color();
            //var dot = Normal * LightDirection;
            //if (dot > 0)
            //{
            //    var diffuse = dot * HitObject.Material.LambertCoeff;
            //    return diffuse * HitObject.Material.Color * Light.Color;
            //}
            //else
            //{
            //    return new Color();
            //}
        }



    }
}
