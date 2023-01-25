using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager i;
    [SerializeField] GameObject msgPanel;
    [SerializeField] bool[] onCandles;
    [SerializeField] UIController uIController;
    [SerializeField] EventController eventController;
    [SerializeField] Text msgText;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject dummyCandle;
    [SerializeField] GameObject exitCloseDoor;
    [SerializeField] GameObject exitOpenDoor;
    [SerializeField] GameObject mutuoCandle;
    [SerializeField] GameObject endingManager;
    public bool isMsg = false;
    
    public GameState gameState;
    float time = 0f;
    int timer = 0;
    


    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        gameState = GameState.STOP;
    }


    private void Update()
    {
        
        if(gameState == GameState.START)
        {
            timer = _countDown();
            uIController.UpdateTimer(timer);
            
        }
        else if(gameState == GameState.GAMEOVER)
        {
            _isGameOver();
        }

        
        
        
    }

    public void ShowMsg(string msg)
    {
        if (isMsg == false)
        {

            AudioManager.i.PlayButton();
            msgPanel.SetActive(true);
            msgText.text = msg;
            StartCoroutine("Pause");
            isMsg = true;
        }
        else
        {
            msgPanel.SetActive(false);
            isMsg = false;
        }

    }

    public void HideMsg()
    {

        if (isMsg == true)
        {

            msgPanel.SetActive(false);
            isMsg = false;
        }
    }

    private IEnumerator Pause()
    {
        PlayerController.ChangeState(PlayerState.STOP);
        yield return new WaitForSeconds(0.5f);
        PlayerController.ChangeState(PlayerState.IDLE);
    }

    public void OnCandle()
    {
        foreach (var (onCandle, index) in onCandles.Select((onCandle, index) => (onCandle, index)))
        {
            if (!onCandle)
            {
                onCandles[index] = true;
                uIController.OnCandle(index);
                if(index == onCandles.Length -1 )
                {
                    _allCandles();
                }
                return;
            }
        }
    }

    private void _allCandles()
    {

        
        AudioManager.i.PlayDoor();
        exitCloseDoor.SetActive(false);
        exitOpenDoor.SetActive(true);
        mutuoCandle.SetActive(true);
        endingManager.SetActive(true);
        StartCoroutine(_openExitMessage());
    }

    private IEnumerator _openExitMessage()
    {
        ShowMsg("‚Æ‚Ñ‚ç‚Ì‚Ð‚ç‚­‚¨‚Æ‚ª‚µ‚½");
        yield return new WaitForSeconds(1f);
        HideMsg();
    }

    private void _reCandle()
    {
        foreach (var (onCandle, index) in onCandles.Select((onCandle, index) => (onCandle, index)))
        {
            if (onCandle)
            {
                uIController.ReCandle(index);
            }
        }
    }



    private void _isGameOver()
    {
        PlayerController.ChangeState(PlayerState.STOP);
        
        SpriteRenderer playerSp = playerObj.GetComponent<SpriteRenderer>();
        playerSp.sortingLayerName = "UI";
        playerSp.color = new Color(0, 0, 0, 1f);
        gameOverPanel.SetActive(true);
        uIController.gameObject.SetActive(false);
        eventController.OnPanelClick();
        HideMsg();
    }

    private int _countDown()
    {
        time += Time.deltaTime;
        return  (int)time;
        
    }

    public void Restart()
    {
        
        timer = 0;
        time = 0;
        uIController.gameObject.SetActive(true);
        uIController.ResetTimer();
        gameState = GameState.START;
        PlayerController.ChangeState(PlayerState.IDLE);
        SpriteRenderer playerSp = playerObj.GetComponent<SpriteRenderer>();
        playerSp.sortingLayerName = "Default";
        playerSp.color = new Color(1, 1, 1, 1f);
        gameOverPanel.SetActive(false);
        _reCandle();
    }
    public void ToTitle ()
    {
        SceneManager.LoadScene("title");
    }


}
