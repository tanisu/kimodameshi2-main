using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   

    public void MoveCamera(Vector3 currentPos)
    {
        transform.position = new Vector3(currentPos.x,currentPos.y -1,transform.position.z);
    }



}
