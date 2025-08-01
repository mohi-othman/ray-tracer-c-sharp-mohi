﻿using RayTracer.Lights;
using RayTracer.Primitives;

namespace RayTracer.Shaders
{
    public class PhongShader : IShader
    {
        public Color GetColor(Primitive HitObject, Light Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal)
        {
            var result = new Color();
            result += new DiffuseShader().GetColor(HitObject, Light, ViewDirection, LightDirection, Normal);
            result += new SpecularShader().GetColor(HitObject, Light, ViewDirection, LightDirection, Normal);
            return result;
        }
    }
}
