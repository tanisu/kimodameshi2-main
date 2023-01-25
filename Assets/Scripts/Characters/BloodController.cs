using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BloodController : MonoBehaviour
{
    [SerializeField] MapDatas mapDatas;
    [SerializeField] GameObject candle;
    [SerializeField] GameObject flower;
    [SerializeField] SpriteRenderer fallBoy;
    bool se = false;
    bool beHappy = false;
    float timer;
    int time = 0;
    int limit = 5;
    public bool isAction = false;
    

    private void Update()
    {
        if (!GameManager.i.isMsg && mapDatas.collects[0])
        {
            if (!se)
            {
                AudioManager.i.PlayEvent_Short();
                se = true;
            }
            if (!beHappy)
            {
                GetComponent<SpriteRenderer>().DOFade(1f, 0.1f).SetLink(gameObject);
            }
            
        }
        
        if(!beHappy && isAction)
        {
            _holdHand();
        }
        else if(!beHappy && !isAction)
        {
            timer = 0;
        }
    }

    private void _holdHand()
    {
        timer += Time.deltaTime;
        time = (int)timer;
        
        if(time > limit)
        {
            
            StartCoroutine("_beHappy");
                
        }
        
    }

    IEnumerator _beHappy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        bool flg = false;
        PlayerController.ChangeState(PlayerState.STOP);

        GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).SetLink(gameObject).OnComplete(() =>
        {
            
            GameManager.i.ShowMsg("‚ ‚è‚ª‚Æ‚¤");
            fallBoy.DOFade(1f, 1.5f).SetLink(fallBoy.gameObject).OnComplete(() =>
            {
                flower.SetActive(false);
                candle.SetActive(true);
                beHappy = true;
                flg = true;
                fallBoy.DOFade(0f, 1f);
            });

        });
        if (flg)
        {
            yield return new WaitForSeconds(1f);
        }
        PlayerController.ChangeState(PlayerState.IDLE);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
