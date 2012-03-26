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

        public Collision(bool isCollision, Vector3D hitPoint, SolidObject hitObject)
        {
            IsCollision = isCollision;
            HitPoint = hitPoint;
            HitObject = hitObject;
        }
    }
}
