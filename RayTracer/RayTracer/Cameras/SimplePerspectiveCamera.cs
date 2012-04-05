using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Cameras
{
    public class SimplePerspectiveCamera:Camera
    {
        public Vector3D CameraLocation { get; set; }

        public SimplePerspectiveCamera(Vector3D cameraLocation, Vector3D direction, double distance)
        {
            CameraLocation = cameraLocation;
        }
        public override Ray GenerateRay(Vector3D target)
        {
            var direction = target - CameraLocation;
            return new Ray(CameraLocation, direction);
        }

        public override View GetView(int width, int height)
        {
            var halfX = Globals.worldWidth/2;
            var halfY = Globals.worldHeight/2;
            return new View(width, height, new Vector3D(-halfX,halfY,0),new Vector3D(halfX,halfY,0),new Vector3D(-halfX,-halfY,0),new Vector3D(halfX,-halfY,0));
        }
    }
}
