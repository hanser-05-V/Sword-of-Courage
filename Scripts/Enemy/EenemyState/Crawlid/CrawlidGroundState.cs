using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidGroundState : EnemyState
{
    protected Crawlid enemy; // 不同怪物的属性不同，不能全部使用父类变量，只用访问父类提供的方法即可

    private Transform player;
    public CrawlidGroundState(Enemy _enemyBase, EnemyFSM _fsm, string _animatorBoolName,Crawlid _enemy) : base(_enemyBase, _fsm, _animatorBoolName)
    {
        this.enemy = _enemy;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player = PlayerManager.Instance.player.transform;
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        
        //检测到玩家 进入战斗状态
        if(Vector2.Distance(enemy.transform.position, player.position) < enemy.ballteleChenkDistance )
        {
            Debug.Log("检测到玩家,进入战斗状态");
            fsm.ChangeState(CrawlidStateType.Battle.ToString());
        }
    }

}
