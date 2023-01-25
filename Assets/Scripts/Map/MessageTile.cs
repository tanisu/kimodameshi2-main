using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MessageTile : MonoBehaviour
{
    [SerializeField] MessageController msc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            msc.ReadMessage();
        }
    }

    public void ReadCurrentMessage(string msg)
    {
        msc.SetMessage(msg);
        msc.ReadMessage();
    }
}
