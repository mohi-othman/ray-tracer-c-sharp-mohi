﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public abstract class Primitive
    {        

        public Vector3D Location { get; set; }

        public Materials.BaseMaterial Material { get; set; }               
                
        public abstract Collision Intersection(Ray ray);

        public abstract Vector3D GetNormal(Vector3D point);

        
    }
}
