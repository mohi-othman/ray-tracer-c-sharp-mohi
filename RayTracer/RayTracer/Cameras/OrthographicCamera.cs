using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Cameras
{
    class OrthographicCamera : Camera
    {       
        public double Distance { get; set; }

        public OrthographicCamera(double distance)
        {
            Distance = distance;
        }

        public override Ray GenerateRay(Vector3D target)
        {
            return new Ray(new Vector3D(target.x, target.y, -Distance), new Vector3D(0, 0, 0.1));
        }

        public override View GetView(int width, int height)
        {
            var halfX = Globals.worldWidth / 2;
            var halfY = Globals.worldHeight / 2;
            return new View(width, height, new Vector3D(-halfX, halfY, 0), new Vector3D(halfX, halfY, 0), new Vector3D(-halfX, -halfY, 0), new Vector3D(halfX, -halfY, 0));
        }
    }
}
