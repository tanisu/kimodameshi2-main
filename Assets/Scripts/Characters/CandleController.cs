using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public enum CandleState
    {
        OFF,
        ON,
        BLUE,
        MAX
    }
    [SerializeField] CandleState candleState;
    [SerializeField] SpriteMask candleMask;
    Animator animator;
    SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetInteger("candleState", (int)candleState);
    }

    public int GetState()
    {
        return (int)candleState;
    }

    public void ReCandle()
    {
        candleState = CandleState.ON;
        animator.SetInteger("candleState", 1);

    }

    public void ChangeState()
    {
        int nextState = ((int)candleState + 1) % (int)CandleState.MAX;
        if(gameObject.CompareTag("TrueCandle") && candleState == CandleState.ON)
        {
            return;
        }
        candleState = (CandleState)nextState;
        
        animator.SetInteger("candleState", nextState);
        if(candleState > (int)CandleState.OFF)
        {
            AudioManager.i.PlayCandleOn();
            sp.sortingOrder = 11;
            candleMask.enabled = true;
            if (gameObject.CompareTag("TrueCandle"))
            {
                GameManager.i.OnCandle();
            }
        }
        else
        {
            sp.sortingOrder = 2;
            candleMask.enabled = false;
        }
    }
}
