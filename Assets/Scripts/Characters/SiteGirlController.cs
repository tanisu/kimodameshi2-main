using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SiteGirlController : MonoBehaviour
{

    [SerializeField] GameObject[] foots;
    [SerializeField] GameObject[] closeDoors;
    [SerializeField] GameObject[] openDoors;
    [SerializeField] GameObject candle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(_showFoot(collision));
        }
    }

    IEnumerator _showFoot(Collider2D collision)
    {
        PlayerController.ChangeState(PlayerState.STOP);
        GetComponent<CircleCollider2D>().enabled = false;
        GameManager.i.ShowMsg("Ç†Ç¢Ç¬Å@Ç¢Ç¡ÇΩÇÊ");
        yield return new WaitForSeconds(1.5f);
        
        GameManager.i.HideMsg();
        for (int i = 0; i < foots.Length; i++)
        {
            AudioManager.i.PlayEvent_Short();
            foots[i].SetActive(true);
            yield return new WaitForSeconds(2f);
        }
        PlayerController.ChangeState(PlayerState.IDLE);
        GetComponent<SpriteRenderer>().DOFade(0f, 2.5f).SetEase(Ease.Flash, 11).SetLink(gameObject);
        StartCoroutine("_openDoor");
    }

    IEnumerator _openDoor()
    {
        AudioManager.i.PlayDoor();
        candle.SetActive(true);
        for (int i = 0;i < closeDoors.Length; i++)
        {
            closeDoors[i].SetActive(false);
            openDoors[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
