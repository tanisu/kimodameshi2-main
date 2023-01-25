using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class MapDatas : MonoBehaviour
{
    public string text;
    public bool hasDoors;
    public bool hasActors;
    public bool hasMsgFlag;
    public bool[] collects;
    public GameObject[] messages;
    public GameObject[] items;
    public GameObject[] doors;
    public GameObject[] actors;
    private bool isMessage = false;

    private void CheckCollect()
    {
        bool result = collects.All(values => values == true);
        if (result && hasActors)
        {
            foreach(GameObject actor in actors)
            {
                actor.GetComponent<GirlController>().ViewGirl();
            }
        }
        if(result && hasDoors)
        {
            
            Invoke("OpenDoor", 1.2f);
            
        }

    }

    void OpenDoor()
    {
        doors[0].SetActive(false);
        AudioManager.i.PlayDoor();
        doors[1].SetActive(true);
    }

    public void CollectItem()
    {
        foreach (var (c, index) in collects.Select((c, index) => (c, index)))
        {
            if (collects[index] != true)
            {
                if (!isMessage)
                {
                    AudioManager.i.PlayEvent();
                }
                
                collects[index] = true;
                break;
            }
        }
        CheckCollect();
    }

    public void ShowMessage(string m_name)
    {
        foreach(var( m,index) in messages.Select((m,index)=>(m,index)))
        {
            if(m.name == m_name)
            {
                m.GetComponent<MessageController>().ReadMessage();
                if (m.GetComponent<MessageController>().isCollect)
                {
                    isMessage = true;
                    CollectItem();
                    return;
                }
                viewItems(index);
                //m.gameObject.layer = 12;
            }
        }
    }

    private void viewItems(int index)
    {
        //メッセージ読んだら立つフラグ処理（アイテム出現）
        if (!items[index].activeSelf)
        {
            items[index].SetActive(true);
        }
    }
}
