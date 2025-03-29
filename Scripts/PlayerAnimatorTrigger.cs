using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    [Header("组件相关")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject attackRightPrefab;
    [SerializeField] private Transform attackEffectParent;
    private GameObject attackObj;

    private void MoveBeforeToMove() // 转为Move状态
    {
        playerController.ChangeState(StateType.Move);
    }

    private void TurnToIdle() // 转为Idle状态
    {
        playerController.ChangeState(StateType.Idle);
        
    }
    private void CreatAttackEffect()
    {
        attackObj = GameObject.Instantiate(attackRightPrefab);
        attackObj.transform.SetParent(attackEffectParent);

    }
    private void DestoryAttackEffect()
    {
        GameObject.Destroy(attackObj);
    }
}
