using UnityEngine;
using System;
using CustomMath;

namespace CustomMath
{
    [System.Serializable]
    public struct PlaneSegment
    {
        #region Variables
        Vec3 upDirection; //the y direction relative to the center NOT TO THE WORLD
        Vec3 rightDirection; //the y direction relative to the center NOT TO THE WORLD
        public Vec3 center;
        public float height;
        public float width;
        public Vec3 TopRightPoint { get { return center + upDirection * height / 2 + rightDirection * width / 2; } }
        public Vec3 TopLeftPoint { get { return center + upDirection * height / 2 - rightDirection * width / 2; } }
        public Vec3 BottomRightPoint { get { return center - upDirection * height / 2 + rightDirection * width / 2; } }
        public Vec3 BottomLeftPoint { get { return center - upDirection * height / 2 - rightDirection * width / 2; } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Constructors
        public PlaneSegment(Vec3 up, Vec3 right, Vec3 planeCenter, float planeHeight, float planeWidth)
        {
            upDirection = up;
            rightDirection = right;
            center = planeCenter;
            height = planeHeight;
            width = planeWidth;
        }
        #endregion

        #region Functions
        public Plane GeneratePlane()
        {
            return new Plane(TopRightPoint, TopLeftPoint, BottomLeftPoint);
        }
        public void UpdatePlane(ref Plane oldPlane)
        {
            oldPlane.Set3Points(TopRightPoint, TopLeftPoint, BottomLeftPoint);
        }
        public void DrawGizmo()
        {
            Gizmos.DrawLine(TopLeftPoint, TopRightPoint);
            Gizmos.DrawLine(BottomLeftPoint, BottomRightPoint);

            Gizmos.DrawLine(BottomLeftPoint, TopLeftPoint);
            Gizmos.DrawLine(BottomRightPoint, TopRightPoint);
        }
        #endregion
    }
}