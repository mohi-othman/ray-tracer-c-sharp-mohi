using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Matrices
{
    public class Vector3DMatrix
    {
        public double[] mat = new double[4];

        public void FromPoint(Vector3D p)
        {
            mat[0] = p.x;
            mat[1] = p.y;
            mat[2] = p.z;
            mat[3] = 1;
        }

        public void FromVector(Vector3D v)
        {
            mat[0] = v.x;
            mat[1] = v.y;
            mat[2] = v.z;
            mat[3] = 0;
        }

        public Vector3D ToVector3D()
        {
            return new Vector3D(mat[0], mat[1], mat[2]);
        }

        public Vector3DMatrix Transform(TransformationMatrix vmatrix)
        {
            var result = new Vector3DMatrix();

            result.mat[0] = mat[0] * vmatrix.mat[0, 0] + mat[1] * vmatrix.mat[0, 1] + mat[2] * vmatrix.mat[0, 2] + mat[3] * vmatrix.mat[0, 3];
            result.mat[1] = mat[0] * vmatrix.mat[1, 0] + mat[1] * vmatrix.mat[1, 1] + mat[2] * vmatrix.mat[1, 2] + mat[3] * vmatrix.mat[1, 3];
            result.mat[2] = mat[0] * vmatrix.mat[2, 0] + mat[1] * vmatrix.mat[2, 1] + mat[2] * vmatrix.mat[2, 2] + mat[3] * vmatrix.mat[2, 3];
            result.mat[3] = mat[0] * vmatrix.mat[3, 0] + mat[1] * vmatrix.mat[3, 1] + mat[2] * vmatrix.mat[3, 2] + mat[3] * vmatrix.mat[3, 3];

            return result;
        }

        public Vector3DMatrix TransformNormal(TransformationMatrix vmatrix)
        {
            var tiMatrix = vmatrix.GetInverse().GetTranspose();

            return Transform(tiMatrix);
        }


    }
}
