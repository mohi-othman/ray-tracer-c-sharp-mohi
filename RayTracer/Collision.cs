using RayTracer.Primitives;

namespace RayTracer
{
    public class Collision
    {
        public bool IsCollision { get; set; }
        public bool IsInsidePrimitive { get; set; }
        public Primitive HitObject { get; set; }
        public double Distance { get; set; }
        public Vector3D Normal { get; set; }
        public Vector3D HitPoint { get; set; }

        public Collision(bool isCollision)
        {
            IsCollision = isCollision;
            IsInsidePrimitive = false;
            HitObject = null;
            Distance = Globals.infinity;
            Normal = null;
            HitPoint = null;
        }
        public Collision(bool isCollision, bool isInsidePrimitive, Primitive hitObject, double distance, Vector3D normal, Vector3D hitPoint)
        {
            IsCollision = isCollision;
            IsInsidePrimitive = isInsidePrimitive;
            HitObject = hitObject;
            Distance = distance;
            Normal = normal;
            HitPoint = hitPoint;
        }
    }
}
