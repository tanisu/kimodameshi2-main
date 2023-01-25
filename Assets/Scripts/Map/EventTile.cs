using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : MonoBehaviour
{
    [SerializeField] GameObject target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target.GetComponent<TargetController>().OnAction();
            gameObject.SetActive(false);
        }
    }
}
