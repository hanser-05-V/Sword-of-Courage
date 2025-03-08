using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Enemy_Skeleton enemy;

    private Transform player;
    public SkeletonGroundState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animatorBoolName)
    {
        this.enemy = enemy;
    }

    public override void OnEntry()
    {
        base.OnEntry();
        //Ŀǰû�д��� PlayerManager ��ʱ����ʵ�ֹ��� 
        //player = GameObject.FindWithTag("Player").transform;
        player = PlayerManager.Instance.player.transform;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if ( Vector2.Distance(player.position,enemy.transform.position)<=3)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
       
    }
}
