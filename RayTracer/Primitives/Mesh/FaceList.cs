using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Primitives.Mesh
{
    public class FaceList
    {
        public List<Vector3D> Vertices { get; set; }
        public List<int[]> Faces { get; set; }
        public List<Vector3D> VertexNormals { get; set; }

        public FaceList()
        {
            Vertices = new List<Vector3D>();
            Faces = new List<int[]>();
            VertexNormals = new List<Vector3D>();
        }
    }
}
