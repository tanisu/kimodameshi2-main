using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetController : MonoBehaviour
{


    enum TargetType
    {
        MOVE,
        BLINK,
        APPEARANCE,
        NONE
    }
    [SerializeField] TargetType targetType;
    [SerializeField] Vector3 endPosition;
    [SerializeField] float duration;
    [SerializeField] bool isDestroy;
    [SerializeField] bool isStop;
    [SerializeField] bool hasNextTarget;
    [SerializeField] GameObject nextTarget;


    public void OnAction()
    {
        
        switch (targetType)
        {
            case TargetType.MOVE:
                _moveAction();
                break;
            case TargetType.BLINK:
                break;
            case TargetType.APPEARANCE:
                _appearance();
                break;
            default:
                break;
        }
    }

    private void _appearance()
    {
        gameObject.SetActive(true);
        if (hasNextTarget)
        {
            nextTarget.SetActive(true);
        }
    }

    private void _moveAction()
    {
        if (isStop)
        {
            PlayerController.ChangeState(PlayerState.STOP);
        }
        transform.DOMove(endPosition, duration).SetEase(Ease.InQuint).SetLink(gameObject).OnComplete(()=> {
            if (hasNextTarget)
            {
                nextTarget.SetActive(true);
            }
            if (isDestroy)
            {
                AudioManager.i.PlayDoor();
                if (isStop)
                {
                    PlayerController.ChangeState(PlayerState.IDLE);
                }
                Destroy(this.gameObject);
                
            }
        });
    }
}
