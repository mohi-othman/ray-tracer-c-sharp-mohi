using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class Collision
    {
        public bool IsCollision { get; set; }
        public Vector3D HitPoint { get; set; }
        public SolidObject HitObject { get; set; }
        public double Distance { get; set; }

        public Collision(bool isCollision, Vector3D hitPoint, SolidObject hitObject, double distance)
        {
            IsCollision = isCollision;
            HitPoint = hitPoint;
            HitObject = hitObject;
            Distance = distance;
        }

        public Vector3D GetNormal()
        {
            return HitObject.GetNormal(HitPoint);
        }
    }
}
