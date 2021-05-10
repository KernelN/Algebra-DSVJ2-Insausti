using UnityEngine;
using CustomMath;

public class Ej2_CubeLoader : MonoBehaviour
{
    //cube
    const short sides = 6;
    CustomPlane[] cubeSide = new CustomPlane[sides];

    //object to render
    bool isInside;
    [SerializeField] GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        //set position for the sides of the cube and make
        cubeSide[0].SetNormalAndPosition(Vec3.Forward, new Vec3(0, 0, -3)); //back plane is in the back and points to the front
        cubeSide[1].SetNormalAndPosition(Vec3.Back, new Vec3(0, 0, 3)); //front plane is in the front and points to the back

        cubeSide[2].SetNormalAndPosition(Vec3.Right, new Vec3(-3, 0, 0)); //left plane is in the left and points to the right
        cubeSide[3].SetNormalAndPosition(Vec3.Left, new Vec3(3, 0, 0)); //right plane is in the right and points to the left

        cubeSide[4].SetNormalAndPosition(Vec3.Up, new Vec3(0, -3, 0)); //bottom plane is in the bottom and points to the top
        cubeSide[5].SetNormalAndPosition(Vec3.Down, new Vec3(0, 3, 0)); //top plane is in the top and points to the bottom
    }

    // Update is called once per frame
    void Update()
    {
        isInside = true;
        for (int i = 0; i < sides; i++)
        {
            //only set false if false prevents the game from using only the last plane
            if (!cubeSide[i].GetSide(obj.transform.position)) 
            {
                isInside = false;
            }
        }

        obj.GetComponent<MeshRenderer>().enabled = isInside;
    }
}