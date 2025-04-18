using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidBattleState : EnemyState
{
    
    private Crawlid enemy;

    private int moveDir ; //战斗移动方向  1-右  -1-左
    private Transform player;
    public CrawlidBattleState(Enemy _enemyBase, EnemyFSM _fsm, string _animatorBoolName, Crawlid _enemy) : base(_enemyBase, _fsm, _animatorBoolName)
    {
        this.enemy = _enemy;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入战斗状态");

        player = PlayerManager.Instance.player.transform;

    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnUpdate()
    {

        base.OnUpdate();

        if(enemy.isPlayerDetected()) //检测到玩家 重置战斗持续时间
        {
            stateTime = enemy.balletTime;
        }
        //设置 移动方向
        if(player.position.x > enemy.transform.position.x) //玩家在右边
        {   
            moveDir = 1;
        }
        else if(player.position.x < enemy.transform.position.x) //玩家在左边
        {
            moveDir = -1;
        }

        enemy.SetVecolity(enemy.moveSpeed * moveDir,rb.velocity.y); //设置速度

    }
}
