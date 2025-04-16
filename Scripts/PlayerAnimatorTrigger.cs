using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    [Header("组件相关")]
    [SerializeField] private Player player;
   
    private void MoveBeforeToMove() // 转为Move状态
    {
        player.playerFSM.ChangeState(StateType.Move);
    }

    private void TurnToIdle() // 转为Idle状态
    {
         player.playerFSM.ChangeState(StateType.Idle);
        
    }

    private void AttackTrigger()//结束攻击动画
    {
        player.playerFSM.currentState.triggerCalled = true;
    }

}
