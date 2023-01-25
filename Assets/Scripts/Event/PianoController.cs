using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PianoController : MonoBehaviour
{
    
    [SerializeField] GameObject head;
    [SerializeField] GameObject messageTile;
    [SerializeField] GameObject piano;
    [SerializeField] GameObject bloodPiano;
    [SerializeField] GameObject bloodPiano2;

    [SerializeField] GameObject mutsuo;

    [SerializeField] EventController eventController;
    

    Keys[] keys;
    List<string> playedNote = new List<string>();
    List<string> collectNote = new List<string>() { "G","A","E","E"};
    private int limitPlay = 3;
    private int playTime = 0;
    private bool collect = false;
    private string[] msg = new string[4]{ "なにかきこえた！　そらみみ？", "あしおとがきこえる！そらみみ？","そらみみじゃない！だれかいる！","「へただな」"};

    private void Start()
    {
        keys = GetComponentsInChildren<Keys>();
    }

    public void PlayPiano(string key)
    {
        foreach(var( k,idx) in keys.Select((k,idx)=>(k,idx)))
        {
            if(k.name == key)
            {
                AudioManager.i.PlayPiano(idx);
                playedNote.Add(key);
            }
        }

        if (playedNote.SequenceEqual(collectNote) && !collect )
        {
            collect = true;
            StartCoroutine(_collectNote());
        }
    }

    public void ResetList()
    {
        if (!collect)
        {
            playTime++;
            if(playTime > limitPlay)
            {
                messageTile.GetComponent<MessageTile>().ReadCurrentMessage(msg[limitPlay]);
            }
            else
            {
                messageTile.GetComponent<MessageTile>().ReadCurrentMessage(msg[playTime]);
            }


            
        }
        playedNote.Clear();
    }


    private IEnumerator _collectNote()
    {
        yield return new WaitForSeconds(1f);
        
        AudioManager.i.PlayEvent();
        messageTile.SetActive(false);
        head.SetActive(true);
        bloodPiano.SetActive(true);
        bloodPiano2.SetActive(true);
        Destroy(piano.GetComponent<EventTriggerController>());
        piano.layer = 0;
        eventController.OnPanelClick();
    }

    
}
