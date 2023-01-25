using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAreaController : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MoveObj"))
        {
            targetObj.SetActive(true);
        }
    }
}
