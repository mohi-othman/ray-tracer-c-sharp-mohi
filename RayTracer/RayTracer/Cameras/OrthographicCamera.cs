using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Cameras
{
    class OrthographicCamera : Camera
    {
        public Vector3D CameraLocation { get; set; }
        public Vector3D Direction { get; set; }
        public Vector3D Up { get; set; }

        public OrthographicCamera(Vector3D location, Vector3D direction, Vector3D up)
        {
            CameraLocation = location;
            Direction = direction;
            Up = up;
        }

        public override Ray GenerateRay(Vector3D target)
        {
            return new Ray(target, Direction);
        }

        public override View GetView(int width, int height, double pixelSize)
        {
            var p1 = TranslatePoint(0, 0, width, height, pixelSize);
            var p2 = TranslatePoint(width - 1, 0, width, height, pixelSize);
            var p3 = TranslatePoint(0, height - 1, width, height, pixelSize);
            var p4 = TranslatePoint(width - 1, height - 1, width, height, pixelSize);

            return new View(width, height, p1, p2, p3, p4);
        }

        private Vector3D TranslatePoint(int x, int y, int width, int height, double pixelSize)
        {
            return CameraLocation +
                    ((Vector3D.Cross(Up, Direction) * (x + (-0.5 * width)) * pixelSize)) -
                    (Up * ((y + (-0.5 * height)) * pixelSize));
        }
    }
}
