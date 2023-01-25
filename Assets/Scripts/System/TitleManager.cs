using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Text titleText;
    public GameObject btn;
    public GameObject startBtn;
    public GameObject skipBtn;
   
    public string[] msg;
    int idx;

    
    void Start()
    {
        StartCoroutine(_typeSentence());
    }

    private void Update()
    {
        
        if(titleText.text == msg[idx])
        {
            if (idx == msg.Length - 1)
            {
                startBtn.SetActive(true);
            }
            else
            {
                btn.SetActive(true);
            }
            
        }
        else
        {
            btn.SetActive(false);
        }
    }

    IEnumerator _typeSentence()
    {
        if(idx == msg.Length - 1)
        {
            skipBtn.SetActive(false);
            titleText.alignment = TextAnchor.MiddleLeft;
            titleText.color = new Color(0.6f,0.0f,0.0f,1f);
        }
        foreach(char c in msg[idx].ToCharArray())
        {
            titleText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void goToNextText()
    {
        if(idx < msg.Length -1)
        {
            idx++;
            titleText.text = "";
            StartCoroutine(_typeSentence());
        }
        else
        {
            titleText.text = "";
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Main");
    }
}
