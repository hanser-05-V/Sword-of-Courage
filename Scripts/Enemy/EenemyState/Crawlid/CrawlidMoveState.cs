using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidMoveState : CrawlidGroundState
{
    public CrawlidMoveState(Enemy _enemyBase, EnemyFSM _fsm, string _animatorBoolName, Crawlid _enemy) : base(_enemyBase, _fsm, _animatorBoolName, _enemy)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
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
