using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    public void OnPanelClick()
    {
        PlayerController.ChangeState(PlayerState.IDLE);
        gameObject.SetActive(false);
        if (panels[0].gameObject.activeSelf)
        {
            panels[0].GetComponent<PianoController>().ResetList();
        }
        _childOff();
    }

    private void _childOff()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void OnChildPanelClick()
    {
        Debug.Log("子パネルクリック");
    }
}
