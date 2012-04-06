using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer.Cameras
{
    public class PerspectiveCamera:Camera
    {
        public Vector3D CameraLocation { get; set; }
        public Vector3D Direction { get; set; }
        public Vector3D Up { get; set; }
        public double Angle { get; set; }

        public PerspectiveCamera(Vector3D cameraLocation, Vector3D direction, Vector3D up, double angle)
        {
            CameraLocation = cameraLocation;
            Direction = direction;
            Up = up;
            Angle = angle;
        }

        public override Ray GenerateRay(Vector3D target)
        {
            var direction = target - CameraLocation;
            return new Ray(CameraLocation, direction.Normalize());
        }

        public override View GetView(int width, int height, double PixelSize)
        {
            var p1 = TranslatePoint(0, 0, width, height, PixelSize);
            var p2 = TranslatePoint(width - 1, 0, width, height, PixelSize);
            var p3 = TranslatePoint(0, height - 1, width, height, PixelSize);
            var p4 = TranslatePoint(width - 1, height - 1, width, height, PixelSize);

            return new View(width, height, p1, p2, p3, p4);
        }

        private Vector3D TranslatePoint(int x, int y, int width, int height, double pixelSize)
        {
            var Distance = width / (2 * Math.Tan(Angle)) * pixelSize;
            return (CameraLocation + (Direction * Distance)) +
                       ((Vector3D.Cross(Up, Direction) * (x + (-0.5 * width)) * pixelSize)) -
                   (Up * ((y + (-0.5 * height)) * pixelSize));
        }
    }
}
