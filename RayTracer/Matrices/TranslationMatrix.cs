using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Matrices
{
    public class TranslationMatrix : TransformationMatrix
    {
        private double _deltaX, _deltaY, _deltaZ;

        public TranslationMatrix(double deltaX, double deltaY, double deltaZ)
        {
            _deltaX = deltaX;
            _deltaY = deltaY;
            _deltaZ = deltaZ;

            mat[0, 0] = 1;
            mat[1, 1] = 1;
            mat[2, 2] = 1;
            mat[3, 3] = 1;
            mat[0, 3] = deltaX;
            mat[1, 3] = deltaY;
            mat[2, 3] = deltaZ;
        }

        public override TransformationMatrix ScaleMatrix(double scalingFactor)
        {
            return new TranslationMatrix(_deltaX * scalingFactor, _deltaY * scalingFactor, _deltaZ * scalingFactor);
        }

    }
}
