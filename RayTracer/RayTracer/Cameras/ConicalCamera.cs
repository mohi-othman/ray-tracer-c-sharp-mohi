using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Cameras
{
    class ConicalCamera:Camera
    {
        public Vector3D CameraLocation { get; set; }

        public ConicalCamera(Vector3D cameraLocation)
        {
            CameraLocation = cameraLocation;
        }
        public override Ray GenerateRay(Vector3D target)
        {
            var direction = target - CameraLocation;
            return new Ray(CameraLocation, direction);
        }
    }
}
