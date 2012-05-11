using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class BezierTranslationMatrix : TransformationMatrix
    {
        private double _deltaX, _deltaY, _deltaZ;
        private Vector3D P1, P2, P3;

        public BezierTranslationMatrix(Vector3D controlPoint1, Vector3D controlPoint2, Vector3D endPoint)
        {
            _deltaX = endPoint.x;
            _deltaY = endPoint.y;
            _deltaZ = endPoint.z;

            P1 = controlPoint1;
            P2 = controlPoint2;
            P3 = endPoint;

            mat[0, 0] = 1;
            mat[1, 1] = 1;
            mat[2, 2] = 1;
            mat[3, 3] = 1;
            mat[0, 3] = _deltaX;
            mat[1, 3] = _deltaY;
            mat[2, 3] = _deltaZ;
        }

        public override TransformationMatrix ScaleMatrix(double scalingFactor)
        {
            var t = scalingFactor;
            if (t > 1)
                t = 1;
            if (t < 0)
                t = 0;
            var P = 3 * Math.Pow(1 - t, 2) * t * P1
                    + 3 * (1 - t) * Math.Pow(t, 2) * P2
                    + Math.Pow(t, 3) * P3;

            _deltaX = P.x;
            _deltaY = P.y;
            _deltaZ = P.z;

            return new TranslationMatrix(_deltaX, _deltaY, _deltaZ);
        }

    }
}
