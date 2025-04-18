using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidIdleState : CrawlidGroundState
{
    public CrawlidIdleState(Enemy _enemyBase, EnemyFSM _fsm, string _animatorBoolName, Crawlid _enemy) : base(_enemyBase, _fsm, _animatorBoolName, _enemy)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        stateTime = enemy.idleTime;

    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(stateTime <=0)
        {
            fsm.ChangeState(CrawlidStateType.Move.ToString());
        }
    }
}
