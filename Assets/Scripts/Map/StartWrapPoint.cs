using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartWrapPoint : WarpManager
{
    
    [SerializeField] GameObject titleLogo;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            AudioManager.i.PlayTitle();
            titleLogo.gameObject.transform.DOMoveY(-0.5f, 6f).SetLink(titleLogo.gameObject).OnComplete(() => {
                _warp(collision.transform);
                collision.gameObject.SetActive(true);
                GameManager.i.gameState = GameState.START;
                Destroy(titleLogo);
                gameObject.SetActive(false);
            });
            
        }
    }

    
}
