using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Lights
{
    public class DirectionalLight : Light
    {
        private Vector3D _direction;

        public DirectionalLight(Vector3D Direction, Vector3D attenuation)
        {
            _direction = Direction.Normalize();
            Attenuation = attenuation;
        }

        public override Vector3D GetLightDirection(Vector3D targetPoint)
        {
            return -1 * _direction;
        }
    }
}
