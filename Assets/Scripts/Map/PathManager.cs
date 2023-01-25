using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    Paths[] paths;
    int currentPathIndex;
    Vector3 currentPos;
    bool mutsuoMove = false;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject mutsuo;
    [SerializeField] GameObject bloodYuka;
    [SerializeField] GameObject eventTile;
    [SerializeField] bool hasAfterItem;
    [SerializeField] bool hasAfterTrigger;
    [SerializeField] string message;


    Animator anim;

    void Start()
    {
        paths = GetComponentsInChildren<Paths>();
        _checkPoint();
        anim = mutsuo.GetComponent<Animator>();
        
    }

    private void _checkPoint()
    {
        if(currentPathIndex == paths.Length)
        {
            
            if (hasAfterItem)
            {
                bloodYuka.SetActive(true);
            }
            
            Destroy(mutsuo);
            if (hasAfterTrigger)
            {
                eventTile.SetActive(true);
            }
            mutsuoMove = false;
            PlayerController.ChangeState(PlayerState.IDLE);
            gameObject.SetActive(false);
        }
        if(currentPathIndex < paths.Length)
        {
            currentPos = paths[currentPathIndex].transform.localPosition;
        }

        
    }
    
    void Update()
    {

        if (!mutsuoMove)
        {
            return;
        }
        PlayerController.ChangeState(PlayerState.STOP);
        if (currentPathIndex < paths.Length)
        {
            if(Vector3.Distance(mutsuo.transform.localPosition, currentPos) > 0.2f)
            {
                if (mutsuo.transform.localPosition.y < paths[currentPathIndex].transform.localPosition.y)
                {
                    anim.SetBool("toBack", true);
                }
                mutsuo.transform.localPosition = Vector3.Lerp(mutsuo.transform.localPosition, currentPos, moveSpeed);
                
            }
            else
            {

                currentPathIndex++;
                _checkPoint();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(message != "")
            {
                StartCoroutine(_showHideMessage());

            }
            else
            {
                mutsuoMove = true;
            }

            
        }
    }

    private IEnumerator _showHideMessage()
    {
        GameManager.i.ShowMsg(message);
        yield return new WaitForSeconds(1.3f);
        GameManager.i.HideMsg();
        if (!mutsuo.activeSelf)
        {
            mutsuo.SetActive(true);
        }
        mutsuoMove = true;
    }

}
