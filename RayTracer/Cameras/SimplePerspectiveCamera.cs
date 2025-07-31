namespace RayTracer.Cameras
{
    public class SimplePerspectiveCamera : ICamera
    {
        public Vector3D CameraLocation { get; set; }
        public Vector3D Direction { get; set; }
        public Vector3D Up { get; set; }
        public double Distance { get; set; }

        public SimplePerspectiveCamera(Vector3D cameraLocation, Vector3D direction, Vector3D up, double distance)
        {
            CameraLocation = cameraLocation;
            Direction = direction;
            Up = up;
            Distance = distance;
        }
        public Ray GenerateRay(Vector3D target)
        {
            var direction = target - CameraLocation;
            return new Ray(CameraLocation, direction.Normalize());
        }

        public View GetView(int width, int height, double PixelSize)
        {
            var p1 = TranslatePoint(0, 0, width, height, PixelSize);
            var p2 = TranslatePoint(width - 1, 0, width, height, PixelSize);
            var p3 = TranslatePoint(0, height - 1, width, height, PixelSize);
            var p4 = TranslatePoint(width - 1, height - 1, width, height, PixelSize);

            return new View(width, height, p1, p2, p3, p4);
        }

        private Vector3D TranslatePoint(int x, int y, int width, int height, double pixelSize)
        {
            return CameraLocation + Direction * Distance +
                        Vector3D.Cross(Up, Direction) * (x + -0.5 * width) * pixelSize -
                    Up * ((y + -0.5 * height) * pixelSize);

        }
    }
}
