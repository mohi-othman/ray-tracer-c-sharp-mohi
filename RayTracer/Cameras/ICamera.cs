namespace RayTracer.Cameras
{
    public interface ICamera
    {
        public abstract Ray GenerateRay(Vector3D target);

        public abstract View GetView(int width, int height, double PixelSize);
    }
}
