using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    [Header("组件相关")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController playerController;


    private void MoveBeforeToMove()
    {
        playerController.ChangeState(StateType.Move);
    }
}
