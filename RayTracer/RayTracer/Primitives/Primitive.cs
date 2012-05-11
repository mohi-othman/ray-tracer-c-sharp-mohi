using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public abstract class Primitive
    {

        public Vector3D Location { get; set; }

        public Materials.Material Material { get; set; }

        public abstract Collision Intersection(Ray ray);

        public abstract Vector3D GetNormal(Vector3D point);

        private List<TransformationMatrix> _transformations;
        public List<TransformationMatrix> Transformations
        {
            get
            {
                if (_transformations == null)
                {
                    _transformations = new List<TransformationMatrix>();
                }
                return _transformations;
            }
            set
            {
                _transformations = value;
            }
        }
    }
}
