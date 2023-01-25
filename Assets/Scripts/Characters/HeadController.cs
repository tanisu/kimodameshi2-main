using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class HeadController : MonoBehaviour
{
    bool viewHead = false;
    [SerializeField] GameObject candle;
    
    void Update()
    {
        
        if (gameObject.activeSelf && !viewHead)
        {
            
            viewHead = true;
            Invoke("_flash", 1);
        }
    }

    private void _flash()
    {
        GetComponent<SpriteRenderer>().DOFade(0f, 2.5f).SetEase(Ease.Flash, 11).SetLink(gameObject).OnComplete(()=> {
            candle.SetActive(true);
            Destroy(gameObject);
        });
    }


}
