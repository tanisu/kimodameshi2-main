using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private float minRotation;
    private float maxRotation;
    
    void Start()
    {
        minRotation = transform.localEulerAngles.z - 45.0f;
        maxRotation = transform.localEulerAngles.z +45.0f;
        //Debug.Log(transform.localEulerAngles.z);
        //this.transform.localRotation = Quaternion.Euler(0f, 0f, maxRotation);
        //transform.localEulerAngles.Set(new Vector3(0f, 0f, maxRotation));
    }

    
    void Update()
    {
        //Debug.Log("a");
        //float currentRotation = transform.localEulerAngles.z;
        //if (transform.localEulerAngles.z > minRotation)
        //{
        //    Debug.Log("aa");
        //    currentRotation -= 0.1f;
        //    transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
        //}
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Debug.Log(minRotation);

        //}
        //if (Input.GetKey(KeyCode.Return))
        //{
        //    Debug.Log("A");
        //}
    }
}
