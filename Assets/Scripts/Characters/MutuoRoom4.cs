using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutuoRoom4 : MonoBehaviour
{
    [SerializeField] GameObject closeDoor;
    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject[] ashiatos;
    private void OnDestroy()
    {
        AudioManager.i.PlayDoor();
        closeDoor.SetActive(false);
        openDoor.SetActive(true);
        foreach(GameObject ashiato in ashiatos)
        {
            ashiato.SetActive(true);
        }
    }
}
