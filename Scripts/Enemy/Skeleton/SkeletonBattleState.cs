using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton enemy;
    private int moveDir;
    private Transform player;
    public SkeletonBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animatorBoolName)
    {
        this.enemy = enemy;
    }

    public override void OnEntry()
    {
        //player = GameObject.FindWithTag("Player");
        player = PlayerManager.Instance.player.transform;
        base.OnEntry();
      
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        //方便处理退出切换
        if (enemy.isPlayerDeleted())
        {
            stateTime = enemy.balletTime;
            if(enemy.isPlayerDeleted().distance < enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if (stateTime <= 0 || Vector2.Distance(player.transform.position,enemy.transform.position)> 7 )
                stateMachine.ChangeState(enemy.idleState);
        }

        if (player.transform.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < enemy.transform.position.x)
            moveDir = -1;
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);

    }
    public bool CanAttack()
    {
        if (Time.time >= enemy.lastAttackTime + enemy.attackCooldown)
        {
            enemy.lastAttackTime = Time.time;
            return true;
        }
        return false;
    }
}
