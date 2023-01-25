using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [SerializeField] string message;
    public bool isCollect;
    public void ReadMessage()
    {
        GameManager.i.ShowMsg(message);   
    }

    public void SetMessage(string msg)
    {
        message = msg;
    }
}
