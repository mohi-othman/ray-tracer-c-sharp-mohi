﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public abstract class SolidObject
    {
        public const double NoColision = -1;

        public Vector3D Location { get; set; }
                
        public abstract double Intersection(Ray ray);


    }
}