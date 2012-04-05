using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public abstract class Camera
    {
        public abstract Ray GenerateRay(Vector3D target);

        public abstract View GetView(int width, int height);
    }
}
