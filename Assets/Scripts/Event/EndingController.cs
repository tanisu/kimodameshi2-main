using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndingController : MonoBehaviour
{
    [SerializeField] GameObject[] fires;
    [SerializeField] GameObject player;
    [SerializeField] GameObject buttonWrapper;
    [SerializeField] GameObject endingTextPanel;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            GameManager.i.gameState = GameState.CLEAR;
            StartCoroutine(_startEnding());
        }
    }

    private IEnumerator _startEnding()
    {
        buttonWrapper.SetActive(false);
        player.GetComponent<PlayerController>().isEnding = true;
        player.transform.DOMoveY(0f, 5f).SetLink(player).OnComplete(()=> {
            player.GetComponent<PlayerController>().isSetPos = true;
            StartCoroutine(_fireStart());
        });
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator _fireStart()
    {
        for (int i = 0; i < fires.Length; i++)
        {
            fires[i].SetActive(true);
            AudioManager.i.PlayCandleOn();
            yield return new WaitForSeconds(1.1f);
        }
        AudioManager.i.PlayEnding();
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < fires.Length; i++)
        {
            fires[i].GetComponent<SpriteRenderer>().sortingOrder = 5;
            
            yield return new WaitForSeconds(0.8f);
            fires[i].transform.DOScale(1.5f, 10f).SetLink(fires[i]).SetEase(Ease.InCubic);
        }
        StartCoroutine(_showEndingText());
    }

    private IEnumerator _showEndingText()
    {
        endingTextPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }
}
