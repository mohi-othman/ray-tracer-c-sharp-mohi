using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Shaders
{
    public abstract class Shader
    {
        public abstract RayTracer.Color GetColor(Primitive HitObject, Light Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal);
               

    }
}
