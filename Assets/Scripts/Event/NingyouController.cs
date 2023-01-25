using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NingyouController : MonoBehaviour
{
    [SerializeField] EventController eventController;
    [SerializeField] GameObject candle;
    [SerializeField] GameObject ningyou;
    [SerializeField] GameObject headDool;
    private Animator animator;
    private SpriteRenderer sp;
    private int tapCount = 0;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    public void OnTap()
    {
        tapCount++;
        
        animator.SetInteger("tapCount",tapCount);
        if(tapCount >= 3)
        {
            StartCoroutine(_dropHead());
        }
    }

    private IEnumerator _dropHead()
    {
        ningyou.SetActive(false);
        sp.color = new Color(1, 0, 0, 1);
        AudioManager.i.PlayThunder();
        
        yield return new WaitForSeconds(1f);
        candle.SetActive(true);
        eventController.OnPanelClick();
        
        headDool.SetActive(true);
    }
}
