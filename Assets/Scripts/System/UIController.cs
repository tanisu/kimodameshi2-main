using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] CandleController[] candleControllers;
    [SerializeField] Slider s;

    public void OnCandle(int index)
    {
        candleControllers[index].ChangeState();
    }

    public void ReCandle(int index)
    {
        
        candleControllers[index].ReCandle();
        //candleControllers[index].ChangeState();
    }

    public void UpdateTimer(int t)
    {
        if(s.value == 0)
        {
            GameManager.i.gameState = GameState.GAMEOVER;
            return;
        }
        s.value = s.maxValue - t;
    }

    public void ResetTimer()
    {
        s.value = s.maxValue;
    }
}
