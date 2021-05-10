using UnityEngine;
using CustomMath;
using System.Collections.Generic;

public class Ej3_FrustumLoader : MonoBehaviour
{

    //Frustum
    Camera mainCamera;
    Frustum frustum;

    [SerializeField] Vec3 near;
    [SerializeField] Vec3 far;

    [SerializeField] GameObject obj; //object

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.gameObject.GetComponent<Camera>();
        frustum = new Frustum(transform, near, far);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        frustum.DrawGizmo();
    }

    // Update is called once per frame
    void Update()
    {
        frustum.Update();
        CheckItemsInFrustum();
    }

    void CheckItemsInFrustum()
    {
        foreach (var item in FindObjectsOfType<MeshRenderer>())
        {
            item.enabled = frustum.ObjectIsInside(item.transform.position);
        }
    }
}
