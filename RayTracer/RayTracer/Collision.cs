using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class Collision
    {
        public bool IsCollision { get; set; }
        public bool IsInsidePrimitive { get; set; }
        public Vector3D HitPoint { get; set; }
        public Primitive HitObject { get; set; }
        public double Distance { get; set; }
        public Vector3D Normal { get; set; }

        public Collision()
        {
            IsCollision = false;
            IsInsidePrimitive = false;
            HitPoint = null;
            HitObject = null;
            Distance = Globals.infinity;
            Normal = null;
        }
        public Collision(bool isCollision, bool isInsidePrimitive, Vector3D hitPoint, Primitive hitObject, Vector3D normal, double distance)
        {
            IsCollision = isCollision;
            IsInsidePrimitive = isInsidePrimitive;
            HitPoint = hitPoint;
            HitObject = hitObject;
            Distance = distance;
            Normal = normal;
        }        
    }
}
