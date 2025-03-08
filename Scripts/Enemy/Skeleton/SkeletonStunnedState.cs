using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonStunnedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animatorBoolName)
    {
        this.enemy = enemy;
    }

    public override void OnEntry()
    {

        base.OnEntry();
        enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        stateTime = enemy.stunnedTime;
        //SetVelocity自带翻转判断 希望他不反转
        //enemy.SetVelocity(enemy.stunnedDir.x * - enemy.faceDir , enemy.stunnedDir.y);
        rb.velocity = new Vector2(enemy.stunnedDir.x * -enemy.faceDir, enemy.stunnedDir.y);
    }

    public override void OnExit()
    {
        base.OnExit();
        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(stateTime <= 0)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
