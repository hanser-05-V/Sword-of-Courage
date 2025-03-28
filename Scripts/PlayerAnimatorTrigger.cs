using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    [Header("组件相关")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController playerController;


    private void MoveBeforeToMove() // 转为Move状态
    {
        playerController.ChangeState(StateType.Move);
    }

    private void TurnToIdle() // 转为Idle状态
    {
        playerController.ChangeState(StateType.Idle);
        
    }
}
