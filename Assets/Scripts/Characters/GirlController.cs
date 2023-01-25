using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GirlController : MonoBehaviour
{
    [SerializeField] float moveX;
    [SerializeField] GameObject candle;
    public void ViewGirl()
    {
        gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().DOFade(1, 1f).SetEase(Ease.InCirc);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(WalkGirl(collision));
        }
    }

    private IEnumerator WalkGirl(Collider2D collision)
    {
        PlayerController.ChangeState(PlayerState.STOP);
        GetComponent<CircleCollider2D>().enabled = false;
        Animator a = GetComponent<Animator>();
        a.Play("GirlNamida");
        yield return new WaitForSeconds(2.5f);
        
        transform.DOMoveX(moveX, 2.5f).SetEase(Ease.OutSine).SetLink(gameObject).OnComplete(() =>
        {
            
            GetComponent<SpriteRenderer>().DOFade(0,1f).SetEase(Ease.InCubic).OnComplete(()=> {
                gameObject.SetActive(false);
                candle.SetActive(true);
            });
        });
        PlayerController.ChangeState(PlayerState.IDLE);

    }
    
}
