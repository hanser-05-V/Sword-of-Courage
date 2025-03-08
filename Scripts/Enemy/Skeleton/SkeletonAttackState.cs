using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonAttackState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animatorBoolName)
    {
        this.enemy = enemy;
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }

    public override void OnExit()
    {
        base.OnExit();
        enemy.lastAttackTime = Time.time;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        enemy.SetZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
