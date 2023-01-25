using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManController : MonoBehaviour
{
    [SerializeField] GameObject wall;
    //[SerializeField] GameObject targetObj;

    private void OnDestroy()
    {
        //targetObj.SetActive(true);
        wall.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        Transform tf = wall.transform;
        wall.transform.position = new Vector3(tf.position.x,tf.position.y,-0.1f);
        
    }
}
