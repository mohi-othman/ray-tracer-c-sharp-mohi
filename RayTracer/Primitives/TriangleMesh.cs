using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Primitives
{
    public class TriangleMesh : Primitive
    {
        private BoundingBox _boundingBox;
        private List<Triangle> _triangles = new List<Triangle>();

        public TriangleMesh(Primitives.Mesh.FaceList faceList)
        {
            foreach (int[] face in faceList.Faces)
            {
                var triangle = new Triangle(faceList.Vertices[face[0]], faceList.Vertices[face[1]], faceList.Vertices[face[2]]);
                _triangles.Add(triangle);
            }
            //find bounding box
            var minVector = faceList.Vertices[0];
            var maxVector = faceList.Vertices[0];

            for (int i = 1; i < faceList.Vertices.Count; i++)
            {
                minVector = new Vector3D(Globals.Min(minVector.x, faceList.Vertices[i].x),
                                         Globals.Min(minVector.y, faceList.Vertices[i].y),
                                         Globals.Min(minVector.z, faceList.Vertices[i].z));
                maxVector = new Vector3D(Globals.Max(maxVector.x, faceList.Vertices[i].x),
                                         Globals.Max(maxVector.y, faceList.Vertices[i].y),
                                         Globals.Max(maxVector.z, faceList.Vertices[i].z));
            }

            _boundingBox = new BoundingBox(maxVector, minVector);
        }

        public override Collision Intersection(Ray ray)
        {
            //try hit bounding box
            var bbCollision = _boundingBox.Intersection(ray);

            if (bbCollision.IsCollision)
            {
                var minT = Globals.infinity;
                Collision hit = null;

                foreach (Triangle t in _triangles)
                {
                    var c = t.Intersection(ray);
                    if (c.IsCollision)
                    {
                        if (c.Distance > 0 && c.Distance < minT)
                        {
                            hit = c;
                            minT = c.Distance;
                        }
                    }
                }

                if (hit != null)
                    return new Collision(true, false, this, hit.Distance, hit.Normal, hit.HitPoint);
                else
                    return new Collision(false);
            }
            return new Collision(false);
        }

        public override Vector3D GetNormal(Vector3D point)
        {
            return null;
        }
    }
}
