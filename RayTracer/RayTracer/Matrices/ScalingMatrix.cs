using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class ScalingMatrix : TransformationMatrix
    {
        private double _deltaX, _deltaY, _deltaZ;
        private Vector3D _refPoint;

        public ScalingMatrix(double deltaX, double deltaY, double deltaZ, Vector3D refPoint)
        {
            _deltaX = deltaX;
            _deltaY = deltaY;
            _deltaZ = deltaZ;

            _refPoint = refPoint;

            mat[0, 0] = deltaX;
            mat[1, 1] = deltaY;
            mat[2, 2] = deltaZ;
            mat[3, 3] = 1;
            mat[0, 3] = (1 - deltaX) * refPoint.x;
            mat[1, 3] = (1 - deltaY) * refPoint.y;
            mat[2, 3] = (1 - deltaZ) * refPoint.z;
        }

        public override TransformationMatrix ScaleMatrix(double scalingFactor)
        {
            return new ScalingMatrix((_deltaX - 1) * scalingFactor + 1, (_deltaY - 1) * scalingFactor + 1, (_deltaZ - 1) * scalingFactor + 1, _refPoint);
        }
    }
}
