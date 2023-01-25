using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerController : MonoBehaviour
{
    public GameObject eventParent;
    public GameObject eventTarget;

    public void ViewEvent()
    {
        PlayerController.ChangeState(PlayerState.STOP);
        eventParent.SetActive(true);
        eventTarget.SetActive(true);
    }
}
