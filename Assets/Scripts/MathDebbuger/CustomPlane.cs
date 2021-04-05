using UnityEngine;
using System;
namespace CustomMath
{
    [System.Serializable]
    public struct CustomPlane : IEquatable<CustomPlane>
    {
        #region Variables
        public Vec3 normal { get; set; }
        public CustomPlane flipped { get { throw new NotImplementedException(); } }
        public float distance { get { throw new NotImplementedException(); } }
        #endregion

        #region constants
        //public const float epsilon = 1e-05f;
        #endregion

        #region Constructors
        public CustomPlane(Vec3 inNormal, Vec3 inPoint)
        {
            throw new NotImplementedException();
        }

        public CustomPlane(Vec3 inNormal, float d)
        {
            throw new NotImplementedException();
        }

        public CustomPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "normal = " + normal.ToString() + 
                "   distance = " + distance.ToString() + 
                "   flipped = " + flipped.ToString();
        }
        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            throw new NotImplementedException();
        }
        public void Flip()
        {
            throw new NotImplementedException();
        }
        public float GetDistanceToPoint(Vec3 point)
        {
            throw new NotImplementedException();
        }
        public bool GetSide(Vec3 point)
        {
            throw new NotImplementedException();
        }
        public bool Raycast(Ray ray, out float enter) //DON'T DO
        {
            throw new NotImplementedException();
        }
        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            throw new NotImplementedException();
        }
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            throw new NotImplementedException();
        }
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            throw new NotImplementedException();
        }
        public static CustomPlane Translate(CustomPlane plane, Vec3 translation)
        {
            throw new NotImplementedException();
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
            return normal == other.normal && flipped == other.flipped && distance == other.distance;
        }

        public override int GetHashCode() //?
        {
            return normal.GetHashCode() ^ (flipped.GetHashCode() << 2) ^ (distance.GetHashCode() >> 2);
        }
        #endregion
    }
}