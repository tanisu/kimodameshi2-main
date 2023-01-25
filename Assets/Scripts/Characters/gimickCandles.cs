using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gimickCandles : MonoBehaviour
{
    [SerializeField] GameObject trueCandle;
    [SerializeField] CandleController[] candles;
    CandleController candleController;
    int[] collectNumber = {2,1,2,1 };
    bool[] collects = { false,false,false,false};



    private void Update()
    {
        for(int i = 0;i < candles.Length; i++)
        {
            if(collectNumber[i] == candles[i].GetState())
            {
                collects[i] = true;
            }else if(collectNumber[i] != candles[i].GetState())
            {
                collects[i] = false;
            }
        }
        bool result = collects.All(values => values == true);
        if (result)
        {
            trueCandle.SetActive(true);
        }
        if(trueCandle.GetComponent<CandleController>().GetState() == 1)
        {
            gameObject.SetActive(false);
        }

    }
}
