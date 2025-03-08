using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonDeadState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName, Enemy_Skeleton _enemy) : base(stateMachine, enemyBase, animatorBoolName)
    {
        enemy = _enemy;
    }

    public override void OnEntry()
    {
        base.OnEntry();

        //enemy.gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        //enemy.cr.enabled = false;
      
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
