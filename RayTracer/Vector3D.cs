﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer
{
    public class Vector3D
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Vector3D(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public static double operator *(Vector3D a, Vector3D b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3D operator *(double scalar, Vector3D vector)
        {
            var xd = scalar * vector.x;
            var yd = scalar * vector.y;
            var zd = scalar * vector.z;

            return new Vector3D(xd, yd, zd);
        }

        public static Vector3D operator *(Vector3D vector, double scalar)
        {
            return scalar * vector;
        }

        public static Vector3D operator -(Vector3D start, Vector3D finish)
        {
            var xd = start.x - finish.x;
            var yd = start.y - finish.y;
            var zd = start.z - finish.z;

            return new Vector3D(xd, yd, zd);
        }

        public static Vector3D operator +(Vector3D start, Vector3D finish)
        {
            var xd = start.x + finish.x;
            var yd = start.y + finish.y;
            var zd = start.z + finish.z;

            return new Vector3D(xd, yd, zd);
        }

        public static double Distance(Vector3D start, Vector3D finish)
        {
            var xd = start.x - finish.x;
            var yd = start.y - finish.y;
            var zd = start.z - finish.z;

            return Math.Sqrt(xd * xd + yd * yd + zd * zd);
        }

        public static Vector3D Cross(Vector3D a, Vector3D b)
        {
            var x = a.y * b.z - b.y * a.z;
            var y = a.z * b.x - b.z * a.x;
            var z = a.x * b.y - b.x * a.y;
            return new Vector3D(x, y, z);
        }

        public Vector3D Inverse()
        {
            return new Vector3D(-x, -y, -z);
        }

        public double Magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector3D Normalize()
        {
            var a = Magnitude();
            if (a == 0)
                return this;
            else
                return new Vector3D(x / a, y / a, z / a);
        }

        public double Distance()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }



    }
}
