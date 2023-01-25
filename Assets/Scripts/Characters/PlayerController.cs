using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    List<GameObject> flashlight;
    [SerializeField]
    List<Sprite> fireSprites;
    [SerializeField]
    List<LayerMask> layerMasks;
    [SerializeField] float h_distance = 0.3f;
    [SerializeField] float v_distance = 0.3f;

    private int playerDirection;
    private static int playerState;

    private float speed = 1.2f;

    private List<float> flashlightMinRota;
    private List<float> flashlightMaxRota;
    private List<float> flashlightDefaultRota;
    private List<GameObject> mapList = new List<GameObject>();
    private float currentRota = 0f;
    private float flashlightAngles = 30f;
    private float flashlightMove = 0.5f;
    private Rigidbody2D rb2d;
    private Animator animator;
    private float animSpeed = 0.7f;

    private int h = 0;
    private int v = 0;

    private SpriteRenderer sp;
    private bool[] arrows;
    private bool isPushAction = false;
    
    
    
    //private bool isAction = false;
    private Vector3 lineDirection;
    private Vector3 firstPos;
    private Vector3 secondPos;


    public bool isEnding = false;
    public bool isSetPos = false;



    enum FlashlightAngle
    {
        NONE,
        Q,
        E
    }
    private FlashlightAngle fA;

    void Start()
    {
        
        arrows = new bool[4];
        playerDirection = (int)Direction.U;
        playerState = (int)PlayerState.IDLE;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        _initFlashlightRota();
    }


    
    void Update()
    {
        if (GameManager.i.gameState == GameState.GAMEOVER)
        {
            _stopAnim();
            return;
        }
        else
        {
            _startAnim();
        }
        if (isEnding)
        {
            playerState = (int)PlayerState.STOP;
            
            if (isSetPos)
            {
                animator.SetTrigger("direction_up");
            }
            else
            {
                animator.SetTrigger("direction_down");
            }
            
        }

        if (playerState != (int)PlayerState.STOP )
        {
 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnAction();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                UnAction();
            }





            if (arrows[0] == true || Input.GetKey(KeyCode.DownArrow))
            {
                v = -1;
                GameManager.i.HideMsg();
            }
            else if (arrows[1] == true || Input.GetKey(KeyCode.UpArrow))
            {
                v = 1;
                GameManager.i.HideMsg();
            }
            else if (arrows[2] == true || Input.GetKey(KeyCode.LeftArrow))
            {
                h = -1;
                GameManager.i.HideMsg();
            }
            else if (arrows[3] == true || Input.GetKey(KeyCode.RightArrow))
            {
                h = 1;
                GameManager.i.HideMsg();
            }
            else
            {
                v = 0;
                h = 0;
            }

            if (isPushAction)
            {
                playerState = (int)PlayerState.IDLE;
                animator.enabled = false;
                sp.sprite = fireSprites[playerDirection];
                return;

            }


            if (Mathf.Abs(h) == 1 || Mathf.Abs(v) == 1)
            {
                animator.speed = animSpeed;
                playerState = (int)PlayerState.MOVE;
            }
            else
            {
                animator.speed = 0;
                playerState = (int)PlayerState.IDLE;
            }


            if (h == 1 && v == 0)
            {

                animator.SetTrigger("direction_x");
                playerDirection = (int)Direction.R;

            }
            if (h == -1 && v == 0)
            {
                animator.SetTrigger("direction_x");
                playerDirection = (int)Direction.L;

            }
            if (v == 1 && h == 0)
            {
                animator.SetTrigger("direction_up");
                playerDirection = (int)Direction.U;

            }
            if (v == -1 && h == 0)
            {
                animator.SetTrigger("direction_down");
                playerDirection = (int)Direction.D;
            }



        }

        //ライトの処理ここから
        foreach(GameObject fl in flashlight)
        {
            fl.SetActive(false);
        }
        flashlight[playerDirection].SetActive(true);
        flashlight[playerDirection].transform.localRotation = Quaternion.Euler(0f, 0f, flashlightDefaultRota[playerDirection]);


        bool q = Input.GetKey(KeyCode.Q);
        bool e = Input.GetKey(KeyCode.E);
        fA = FlashlightAngle.NONE;
        if(q && !e)
        {
            fA = FlashlightAngle.Q;
        }
        if(!q && e)
        {
            fA = FlashlightAngle.E;
        }

        if (fA == FlashlightAngle.Q)
        {
            if ((currentRota + flashlight[playerDirection].transform.localEulerAngles.z) < flashlightMaxRota[playerDirection])
            {
                currentRota = currentRota + flashlightMove;
            }
            flashlight[playerDirection].transform.Rotate(new Vector3(0f, 0f, currentRota));
        }
        if (fA == FlashlightAngle.E)
        {
            if ((currentRota + flashlight[playerDirection].transform.localEulerAngles.z) > flashlightMinRota[playerDirection])
            {
                currentRota = currentRota - flashlightMove;
            }
            flashlight[playerDirection].transform.Rotate(new Vector3(0f, 0f, currentRota));
        }
        if (fA == FlashlightAngle.NONE)
        {
            currentRota = 0f;
        }
    }



    private void _initFlashlightRota()
    {
        flashlightMaxRota = new List<float>();
        flashlightMinRota = new List<float>();
        flashlightDefaultRota = new List<float>();
        foreach (GameObject fl in flashlight)
        {
            flashlightMinRota.Add(fl.transform.localEulerAngles.z - flashlightAngles);
            flashlightMaxRota.Add(fl.transform.localEulerAngles.z + flashlightAngles);
            flashlightDefaultRota.Add(fl.transform.localEulerAngles.z);
        }
    }

    private void FixedUpdate()
    {
        lineDirection = new Vector3();
        
        if (playerState == (int)PlayerState.MOVE )
        {
            
            switch (playerDirection)
            {
                case (int)Direction.D:
                    rb2d.velocity = new Vector2(0f, -speed);
                    break;
                case (int)Direction.U:
                    rb2d.velocity = new Vector2(0f, speed);
                    break;
                case (int)Direction.L:
                    transform.localScale = new Vector3(1, 1, 1);
                    rb2d.velocity = new Vector2(-speed, 0f);
                    break;
                case (int)Direction.R:
                    transform.localScale = new Vector3(-1, 1, 1);
                    rb2d.velocity = new Vector2(speed, 0f);
                    break;
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            switch (playerDirection)
            {
                case (int)Direction.D:
                    lineDirection = transform.up * v_distance;
                    firstPos = transform.position - Vector3.right * 0.1f;
                    secondPos = transform.position + Vector3.right * 0.1f;
                    break;
                case (int)Direction.U:
                    lineDirection = transform.up * -v_distance;
                    firstPos = transform.position - Vector3.right * 0.1f;
                    secondPos = transform.position + Vector3.right * 0.1f;
                    break;
                case (int)Direction.L:
                    lineDirection = transform.right * h_distance;
                    firstPos = transform.position - Vector3.up * 0.1f;
                    secondPos = transform.position + Vector3.up * 0.1f;
                    break;
                case (int)Direction.R:
                    lineDirection = transform.right * -h_distance;
                    firstPos = transform.position - Vector3.up * 0.1f;
                    secondPos = transform.position + Vector3.up * 0.1f;
                    break;
            }





            Debug.DrawLine(firstPos, transform.position - lineDirection, Color.blue);
            Debug.DrawLine(secondPos, transform.position - lineDirection, Color.red);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //マップに入った処理
        if (collision.CompareTag("Map"))
        {
            //マップリストに追加
            mapList.Add(collision.gameObject);
            //カメラを新しいマップに移動
            Camera.main.gameObject.GetComponent<CameraController>().MoveCamera(collision.transform.position);
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //マップを出た処理
        if (collision.CompareTag("Map"))
        {
            //マップリストから削除
            mapList.Remove(collision.gameObject);
            
            foreach (GameObject map in mapList)
            {
                
                if (map.name != collision.name)
                {
                    Camera.main.gameObject.GetComponent<CameraController>().MoveCamera(map.transform.position);
                }
            }
            //            _countMap();
        }

    }
    

    public void OnArrows(int d)
    {
        
        arrows[d] = true;
    }
    public void UnArrows(int d)
    {
        
        arrows[d] = false;
    }





    public void OnAction()
    {
        
        if (playerState != (int)PlayerState.STOP)
        {
            isPushAction = true;
            

            
            
            //キャンドル処理
            if (Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[0]) 
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[0]))
            {
                RaycastHit2D candle = _hitCheck(layerMasks[0]);

                CandleController currentCandle = candle.collider.GetComponent<CandleController>();
                currentCandle.ChangeState();
            }
            //アイテム処理
            if (Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[1]) 
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[1]))
            {
                RaycastHit2D item = _hitCheck(layerMasks[1]);
                item.collider.gameObject.SetActive(false);
                MapDatas mp = mapList[0].GetComponent<MapDatas>();
                mp.CollectItem();
            }
            //フラグ関連メッセージ処理
            if (Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[2]) 
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[2]))
            {
                RaycastHit2D msg = _hitCheck(layerMasks[2]);
                MapDatas mp = mapList[0].GetComponent<MapDatas>();
                mp.ShowMessage(msg.collider.gameObject.name);
            }
            //扉処理
            if (Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[3]) 
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[3]))
            {
                GameManager.i.ShowMsg("あかない");
            }
            //血の処理
            if (Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[4]) 
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[4]))
            {
                
                RaycastHit2D bloodHand = _hitCheck(layerMasks[4]);

                bloodHand.collider.gameObject.GetComponent<BloodController>().isAction = true;
            }
            //ショートメッセージ処理
            if(Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[5]) 
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[5]))
            {
                
                RaycastHit2D shortMsg = _hitCheck(layerMasks[5]);

                shortMsg.collider.gameObject.GetComponent<MessageController>().ReadMessage();
            }
            //イベントパネル表示
            if (Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[6]) 
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[6]))
            {
                GameManager.i.HideMsg();
                RaycastHit2D shortMsg = _hitCheck(layerMasks[6]);
                shortMsg.collider.gameObject.GetComponent<EventTriggerController>().ViewEvent();
            }
            //ムツオキャンドル
            if (Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMasks[8])
                || Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMasks[8]))
            {
                RaycastHit2D mutuoCandle = _hitCheck(layerMasks[8]);
                mutuoCandle.collider.GetComponent<MutuoCandles>().OnCandle();
            }
        }
    }

    private RaycastHit2D _hitCheck(LayerMask layerMask)
    {
        RaycastHit2D hit = Physics2D.Linecast(firstPos, transform.position - lineDirection, layerMask);
        
        if(!hit)
        {
            hit = Physics2D.Linecast(secondPos, transform.position - lineDirection, layerMask);
        }

        return hit;
    }
  

    public void UnAction()
    {
        isPushAction = false;
        
        //血の処理
        if (Physics2D.Linecast(transform.position, transform.position - lineDirection, layerMasks[4]))
        {
            RaycastHit2D bloodHand = Physics2D.Linecast(transform.position, transform.position - lineDirection, layerMasks[4]);
            bloodHand.collider.gameObject.GetComponent<BloodController>().isAction = false;
        }

    }

    public static void ChangeState(PlayerState state)
    {
        playerState = (int)state;
    }

    public void ChangeDirection()
    {
        playerDirection = (int)Direction.U;
    }



    private void _stopAnim()
    {
        
        animator.enabled = false;
    }
    private void _startAnim()
    {
        
        animator.enabled = true;
    }



}
