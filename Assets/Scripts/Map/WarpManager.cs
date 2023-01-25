using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    
    [SerializeField] Vector2 to;

    protected void _warp(Transform tf)
    {
        //Debug.Log($"to:{to} transform{tf}");
        tf.position = to;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _warp(collision.transform);

        }
    }
}
