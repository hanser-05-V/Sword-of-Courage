using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animatorBoolName, enemy)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
       
        enemy.SetVelocity(enemy.moveSpeed * enemy.faceDir, enemy.rb.velocity.y);
        if(enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Filp();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
