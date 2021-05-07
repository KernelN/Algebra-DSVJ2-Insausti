//mira Lean, no entendí un choto, así que lo hice con Gonza <3

using UnityEngine;
using System;
namespace CustomMath
{
    [System.Serializable]
    public struct CustomPlane : IEquatable<CustomPlane>
    {
        #region Variables
        public Vec3 normal { get; set; }
        public CustomPlane flipped { get { return new CustomPlane(-normal, distance); } }
        public float distance { get; set; }
        #endregion

        #region constants
        //public const float epsilon = 1e-05f;
        #endregion

        #region Constructors
        public CustomPlane(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = Vec3.Project(inPoint, inNormal).magnitude;
        }

        public CustomPlane(Vec3 inNormal, float d)
        {
            normal = inNormal.normalized;
            distance = d;
        }

        public CustomPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 aux = Vec3.Cross(b - a, c - a); //Cross devuelve vector perpendicular a 2 vectores
            normal = aux.normalized;
            distance = aux.magnitude;
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "normal = " + normal.ToString() +
                "   distance = " + distance.ToString() +
                "   flipped = " + flipped.ToString();
        }
        public Vec3 ClosestPointOnPlane(Vec3 point) //MUST CHECK
        {
            return normal * distance + point;
        }
        public void Flip()
        {
            normal *= -1;
        }
        public float GetDistanceToPoint(Vec3 point) //MUST CHECK
        {
            return (ClosestPointOnPlane(point) - point).magnitude;
        }
        public bool GetSide(Vec3 point) //MUST CHECK
        {
            Vec3 closestPoint = ClosestPointOnPlane(point);
            Vec3 sideTrue = Vec3.Project(point, closestPoint.normalized) - point; //Project+ & point distance
            Vec3 sideFalse = Vec3.Project(point, -closestPoint.normalized) - point; //Project- & point distance
            return sideTrue.sqrMagnitude < sideFalse.sqrMagnitude; //sideTrue is shorter than sideFalse
        }
        public bool Raycast(Ray ray, out float enter) //DON'T DO
        {
            throw new NotImplementedException();
        }
        public bool SameSide(Vec3 inPt0, Vec3 inPt1) //optimize when checked
        {
            return GetSide(inPt0) == GetSide(inPt1);
        }
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c) //optimize when checked
        {
            this = new CustomPlane(a, b, c); 
        }
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)  //optimize when checked
        {
            this = new CustomPlane(inNormal, inPoint);
        }
        public static CustomPlane Translate(CustomPlane plane, Vec3 translation)
        {
            Vec3 point = plane.normal * plane.distance; //creates a point in the center of the pane
            point += translation; //move point to new position (using the offset)
            return new CustomPlane(point.normalized, point.magnitude); //create a new plane using the new point
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is CustomPlane)) return false;
            return Equals((CustomPlane)other);
        }

        public bool Equals(CustomPlane other)
        {
            return normal == other.normal && distance == other.distance;
        }

        public override int GetHashCode() //?
        {
            return normal.GetHashCode() ^ (flipped.GetHashCode() << 2) ^ (distance.GetHashCode() >> 2);
        }
        #endregion
    }
}