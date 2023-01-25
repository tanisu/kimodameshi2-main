using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MutuoCandles : MonoBehaviour
{
    
    [SerializeField] SpriteMask candleMask;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mutuo;
    [SerializeField] GameObject mutuoEndingPanel;
    [SerializeField] GameObject buttonWrapper;
    [SerializeField] GameObject[] bgPanels;
    [SerializeField] GameObject lastPanel;
    [SerializeField] GameObject mutuoEndingTextPanel;
    [SerializeField] Sprite sp;
    Animator animator;
    
    PlayerController pl;
    SpriteRenderer r;
    private float interval = 0.3f;
    private float bgInterval = 3.3f;
    private int currentIdx = 0;

    void Start()
    {
        
        animator = GetComponent<Animator>();
        pl = player.GetComponent<PlayerController>();
        r = player.GetComponent<SpriteRenderer>();
    }

    public void OnCandle()
    {
        GameManager.i.gameState = GameState.CLEAR;
        animator.SetBool("MutuoCandle",true);
        candleMask.enabled = true;
        StartCoroutine(_mutuoEnding());
    }

    private IEnumerator _mutuoEnding()
    {
        pl.isEnding = true;
        GameManager.i.ShowMsg("「それは、ぼくのだ」");
        yield return new WaitForSeconds(1.5f);
        
        GameManager.i.HideMsg();
        yield return new WaitForSeconds(1.5f);
        
        pl.ChangeDirection();
        pl.isSetPos = true;
        AudioManager.i.PlayDoor();
        mutuo.SetActive(true);
        yield return new WaitForSeconds(0.53f);
        mutuo.transform.DOMove(player.transform.position, 7f).SetLink(mutuo).OnComplete(()=> {
            GameManager.i.ShowMsg("「きみは、ぼくだよ」");
            StartCoroutine(_endingStart());
        });
    }

    private IEnumerator _endingStart()
    {
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        Camera.main.DOOrthoSize(1.64f, 3f);
        yield return new WaitForSeconds(3.3f);
        GameManager.i.HideMsg();
        

        while(interval > 0.0001f)
        {
            r.enabled = !r.enabled;
            interval *= 0.95f;
            yield return new WaitForSeconds(interval);
        }
        
        //gameObject.SetActive(false);
        mutuo.SetActive(false);
        player.SetActive(false);
        mutuoEndingPanel.SetActive(true);
        buttonWrapper.SetActive(false);
        //yield return new WaitForSeconds(5f);

        StartCoroutine(_changeBGImage());
    }

    private IEnumerator _changeBGImage()
    {
        
        int bgImageCount = bgPanels.Length;
        
        while (bgInterval > 0.00001f)
        {
            AudioManager.i.PlayWalk();
            bgPanels[currentIdx].gameObject.SetActive(true);
            int prevIdx = currentIdx;
            currentIdx++;
            currentIdx = currentIdx % bgImageCount;
            bgInterval *= 0.891f;
            yield return new WaitForSeconds(bgInterval);
            bgPanels[prevIdx].gameObject.SetActive(false);
        }
        lastPanel.SetActive(true);
        yield return new WaitForSeconds(1.12f);
        mutuoEndingPanel.transform.Find("PlayerSilet").GetComponent<SpriteRenderer>().sprite = sp;
        yield return new WaitForSeconds(0.52f);
        mutuoEndingPanel.transform.Find("PlayerSilet").gameObject.SetActive(false);
        mutuoEndingTextPanel.SetActive(true);


    }
}
