using RayTracer.Lights;
using RayTracer.Primitives;

namespace RayTracer.Shaders
{
    public interface IShader
    {
        public RayTracer.Color GetColor(Primitive HitObject, Light Light, Vector3D ViewDirection, Vector3D LightDirection, Vector3D Normal);
    }
}
