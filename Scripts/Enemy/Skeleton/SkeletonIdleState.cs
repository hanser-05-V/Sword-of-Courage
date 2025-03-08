using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animatorBoolName, enemy)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
        stateTime = enemy.idleTime;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
     
        base.OnUpdate();
        if(stateTime < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
