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
        public Primitive HitObject { get; set; }
        public double Distance { get; set; }        

        public Collision(bool isCollision)
        {
            IsCollision = isCollision;
            IsInsidePrimitive = false;            
            HitObject = null;
            Distance = Globals.infinity;            
        }
        public Collision(bool isCollision, bool isInsidePrimitive, Primitive hitObject, double distance)
        {
            IsCollision = isCollision;
            IsInsidePrimitive = isInsidePrimitive;            
            HitObject = hitObject;
            Distance = distance;            
        }        
    }
}
