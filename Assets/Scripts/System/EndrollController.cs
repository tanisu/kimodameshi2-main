using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndrollController : MonoBehaviour
{
    [SerializeField] Text headerText;
    [SerializeField] Text nameText;
    [SerializeField] Text endText;
    [SerializeField] Sprite[] sps;
    [SerializeField] string[] names;
    [SerializeField] string[] staffHead;
    [SerializeField] string staffname;
    


    [SerializeField] SpriteRenderer spr;
    
    void Start()
    {
        //AudioManager.i.PlayEnding();
        StartCoroutine(_changeCast());
    }

    private IEnumerator _changeCast()
    {

        for(int i = 0;i < sps.Length; i++)
        {
            spr.sprite = sps[i];
            nameText.text = names[i];
            yield return new WaitForSeconds(3f);
        }
        StartCoroutine(_changeStaff());
    }

    private IEnumerator _changeStaff()
    {
        nameText.text = staffname;
        for (int i = 0;i < staffHead.Length; i++)
        {
            headerText.text = staffHead[i];
            yield return new WaitForSeconds(3f);
        }
        headerText.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        spr.enabled = false;
        endText.gameObject.SetActive(true);
    }

}
