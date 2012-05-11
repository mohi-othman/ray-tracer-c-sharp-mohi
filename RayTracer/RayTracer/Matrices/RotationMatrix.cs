using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    //http://www.fastgraph.com/makegames/3drotation/

    public class RotationMatrix : TransformationMatrix
    {
        private double _theta;
        private Vector3D _rotationAxis;
        private Vector3D _rotationPoint;

        public RotationMatrix(double theta, Vector3D rotationAxis, Vector3D rotationPoint)
        {
            _theta = theta;
            _rotationAxis = rotationAxis.Normalize();
            _rotationPoint = rotationPoint;

            double x, y, z, x2, y2, z2, c, s, t;
            x = _rotationAxis.x;
            y = _rotationAxis.y;
            z = _rotationAxis.z;
            x2 = Math.Pow(x, 2);
            y2 = Math.Pow(y, 2);
            z2 = Math.Pow(z, 2);
            c = Math.Cos(theta);
            s = Math.Sin(theta);
            t = 1 - c;

            var R = new TransformationMatrix();
            R.mat[0, 0] = t * x2 + c;
            R.mat[1, 0] = t * x * y - s * z;
            R.mat[2, 0] = t * x * z + s * y;

            R.mat[0, 1] = t * x * y + s * z;
            R.mat[1, 1] = t * y2 + c;
            R.mat[2, 1] = t * y * z - s * x;

            R.mat[0, 2] = t * x * z - s * y;
            R.mat[1, 2] = t * y * z + s * x;
            R.mat[2, 2] = t * z2 + c;
        
            R.mat[3, 3] = 1;

            var T = new TranslationMatrix(rotationPoint.x, rotationPoint.y, rotationPoint.z);

            var RT = R * T;

            CopyFrom(RT);
        }
       
        public override TransformationMatrix ScaleMatrix(double scalingFactor)
        {
            return new RotationMatrix(_theta * scalingFactor, _rotationAxis, _rotationPoint);
        }

    }
}
