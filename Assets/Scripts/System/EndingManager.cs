using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public Text titleText;
    public string[] msg;
    int idx;


    void Start()
    {
        StartCoroutine(_typeSentence());

    }
  

    IEnumerator _typeSentence()
    {
        if (idx == msg.Length - 1)
        {
            //skipBtn.SetActive(false);
            titleText.alignment = TextAnchor.MiddleLeft;
            titleText.color = new Color(1f, 0.0f, 0.0f, 1f);

        }
        foreach (char c in msg[idx].ToCharArray())
        {
            titleText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(3.5f);
        goToNextText();
    }

    public void goToNextText()
    {
        if (idx < msg.Length - 1)
        {
            idx++;
            titleText.text = "";
            StartCoroutine(_typeSentence());
        }
        else
        {
            //titleText.text = "";
            
            SceneManager.LoadScene("Endroll");
        }
    }
}
