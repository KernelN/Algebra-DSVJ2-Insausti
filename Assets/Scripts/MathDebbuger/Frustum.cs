using UnityEngine;
using System;
using CustomMath;

namespace CustomMath
{
    [System.Serializable]
    public struct Frustum
    {
        #region Variables

        //Vars to build planes
        Transform frustumOrigin;
        float nearDistance;
        float farDistance;
        public PlaneSegment nearSegment;
        public PlaneSegment farSegment;

        //Planes
        CustomPlane left;
        CustomPlane right;
        CustomPlane bottom;
        CustomPlane top;
        CustomPlane far;
        CustomPlane near;


        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Constructors
        public Frustum(Transform originObj, Vec3 planeNear, Vec3 planeFar) //x = distance, y = height, z = width
        {
            frustumOrigin = originObj; //set the transform which the frustum uses as reference

            //----------BUILD PLANE SEGMENTS------------
            //Set Near PlaneSegment Propieties
            nearDistance = planeNear.x;
            Vec3 centerAux = frustumOrigin.position + frustumOrigin.forward * nearDistance;
            nearSegment = new PlaneSegment(frustumOrigin.up, frustumOrigin.right, centerAux, planeNear.y, planeNear.z);
            //Set Far PlaneSegment Propieties
            farDistance = planeFar.x;
            centerAux = frustumOrigin.position + frustumOrigin.forward * farDistance;
            farSegment = new PlaneSegment(frustumOrigin.up, frustumOrigin.right, centerAux, planeFar.y, planeFar.z);

            //------------BUILD PLANES--------------inverts positive planes's normal because they point outside
            //----------------------------------left
            left = new CustomPlane(farSegment.TopLeftPoint, nearSegment.BottomLeftPoint, originObj.position);
            left.Flip();
            //----------------------------------right
            right = new CustomPlane(farSegment.TopRightPoint, nearSegment.BottomRightPoint, originObj.position);
            //right.Flip();
            //----------------------------------bottom
            bottom = new CustomPlane(farSegment.BottomLeftPoint, nearSegment.BottomRightPoint, originObj.position);
            //----------------------------------top
            top = new CustomPlane(farSegment.TopLeftPoint, nearSegment.TopRightPoint, originObj.position);
            top.Flip();
            //----------------------------------near
            near = nearSegment.GeneratePlane();
            //----------------------------------far
            far = farSegment.GeneratePlane();
            far.Flip();
        }
        #endregion

        #region Functions
        public void Update()
        {
            //----------UPDATE PLANE SEGMENTS------------
            Vec3 auxCenter = frustumOrigin.position + frustumOrigin.forward * nearDistance;
            nearSegment.Update(frustumOrigin.up, frustumOrigin.right, auxCenter);
            auxCenter = frustumOrigin.position + frustumOrigin.forward * farDistance;
            farSegment.Update(frustumOrigin.up, frustumOrigin.right, auxCenter);
            //------------UPDATE PLANES--------------flip positive planes because they point outside of frustum
            //----------------------------------left
            left.Set3Points(farSegment.TopLeftPoint, nearSegment.BottomLeftPoint, frustumOrigin.position);
            left.Flip();
            //----------------------------------right
            right.Set3Points(farSegment.TopRightPoint, nearSegment.BottomRightPoint, frustumOrigin.position);
            //right.Flip();
            //----------------------------------bottom
            bottom.Set3Points(farSegment.BottomLeftPoint, nearSegment.BottomRightPoint, frustumOrigin.position);
            //----------------------------------top
            top.Set3Points(farSegment.TopLeftPoint, nearSegment.TopRightPoint, frustumOrigin.position);
            top.Flip();
            //----------------------------------near
            nearSegment.UpdatePlane(ref near);
            //----------------------------------far
            farSegment.UpdatePlane(ref far);
            far.Flip();
        }
        public bool ObjectIsInside(Vec3 objectPosition)
        {
            Debug.Log("left " + left.GetSide(objectPosition));
            Debug.Log("right " + right.GetSide(objectPosition));
            Debug.Log("bottom " + bottom.GetSide(objectPosition));
            Debug.Log("top " + top.GetSide(objectPosition));
            Debug.Log("near " + near.GetSide(objectPosition));
            Debug.Log("far " + far.GetSide(objectPosition));

            return
                left.GetSide(objectPosition) &&
                right.GetSide(objectPosition) &&
                bottom.GetSide(objectPosition) &&
                top.GetSide(objectPosition) &&
                near.GetSide(objectPosition) &&
                far.GetSide(objectPosition);

            /*
            Debug.Log("left " + left.SameSide(left.normal, objectPosition));
            Debug.Log("right " + right.SameSide(right. normal, objectPosition));
            Debug.Log("bottom " + bottom.SameSide(bottom.normal, objectPosition));
            Debug.Log("top " + top.SameSide(top.normal, objectPosition));
            Debug.Log("near " + near.SameSide(near.normal, objectPosition));
            Debug.Log("far " + far.SameSide(far.normal, objectPosition));

            return
                left.SameSide(left.normal, objectPosition) &&
                right.SameSide(right.normal, objectPosition) &&
                bottom.SameSide(bottom.normal, objectPosition) &&
                top.SameSide(top.normal, objectPosition) &&
                near.SameSide(near.normal, objectPosition) &&
                far.SameSide(far.normal, objectPosition);
            */
        }
        public void DrawGizmo()
        {
            nearSegment.DrawGizmo();
            farSegment.DrawGizmo();

            Gizmos.DrawLine(nearSegment.BottomRightPoint, farSegment.BottomRightPoint);
            Gizmos.DrawLine(nearSegment.BottomLeftPoint, farSegment.BottomLeftPoint);
            Gizmos.DrawLine(nearSegment.TopRightPoint, farSegment.TopRightPoint);
            Gizmos.DrawLine(nearSegment.TopLeftPoint, farSegment.TopLeftPoint);
        }
        #endregion
    }
}