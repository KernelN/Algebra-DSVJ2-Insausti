//Lean, la verdad es que no tenia ni idea de como hacer el plano, asi que hable con Gonza
//Gonza tampoco entendia nada, asi que lo hicimos juntos, por eso el codigo es parecido
//queria avisarte antes de que leas el codigo y digas "Ah, esto ya lo vi"
//'chas gracias por comprender

using UnityEngine;
using System;
namespace CustomMath
{
    [System.Serializable]
    public struct CustomPlane
    {
        #region Variables
        public Vec3 normal { get; set; }
        public CustomPlane flipped { get { return new CustomPlane(-normal, -distance); } }
        public float distance { get; set; } //relative to 0,0,0 AND where the plane points
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
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

        public CustomPlane(Vec3 a, Vec3 b, Vec3 c) //MUST CHECK
        {
            Vec3 aux = Vec3.Cross(b - a, c - a); //Cross returns prependicular vector to 2 vectors
            normal = aux.normalized;
            distance = aux.magnitude;
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "normal = " + normal.ToString() +
                "   distance = " + distance.ToString();
        }
        public Vec3 ClosestPointOnPlane(Vec3 point) //MUST CHECK
        {
            //return normal * distance + point;
            return point - GetDistanceToPoint(point) * normal;
        }
        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }
        public float GetDistanceToPoint(Vec3 point)
        {
            //return (ClosestPointOnPlane(point) - point).magnitude;
            return Vec3.Dot(normal, point) + distance;
        }
        public bool GetSide(Vec3 point)
        {
            /*
            Vec3 closestPoint = ClosestPointOnPlane(point);
            Vec3 sideTrue = Vec3.Project(point, closestPoint.normalized) - point; //Project+ & point distance
            Vec3 sideFalse = Vec3.Project(point, -closestPoint.normalized) - point; //Project- & point distance
            return sideTrue.sqrMagnitude < sideFalse.sqrMagnitude; //sideTrue is shorter than sideFalse
            */

            return  GetDistanceToPoint(point) > -epsilon; //return if distance is bigger than smallest -0,0...01f
        }
        public bool Raycast(Ray ray, out float enter) //DON'T DO
        {
            throw new NotImplementedException();
        }
        public bool SameSide(Vec3 inPt0, Vec3 inPt1) //optimize when checked
        {
            return GetSide(inPt0) == GetSide(inPt1);
        }
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c) //MUST CHECK
        {
            Vec3 aux = Vec3.Cross(b - a, c - a); //Cross returns prependicular vector to 2 vectors
            normal = aux.normalized;
            distance = aux.x * a.x - aux.y * a.y - aux.z * a.z;
            //https://www.youtube.com/watch?v=ijXUQfwL8Kg
        }
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = Vec3.Project(inPoint, inNormal).magnitude;
        }
        public static CustomPlane Translate(CustomPlane plane, Vec3 translation)
        {
            Vec3 point = plane.normal * plane.distance; //creates a point in the center of the pane
            point += translation; //move point to new position (using the offset)
            return new CustomPlane(point.normalized, point.magnitude); //create a new plane using the new point
        }
        #endregion
    }
}