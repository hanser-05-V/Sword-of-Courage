using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrawlidStateType
{
    Idle,
    Move, Battle

}
public class Crawlid : Enemy
{
    
    protected override void Awake()
    {
        base.Awake();
        enemyFSM.enemyStateDic.Add(CrawlidStateType.Idle.ToString(),new CrawlidIdleState(this, enemyFSM, "Idle",this));//第一个this是父类Enenmy，第二个this是自身
        enemyFSM.enemyStateDic.Add(CrawlidStateType.Move.ToString(),new CrawlidMoveState(this, enemyFSM, "Move",this));
        enemyFSM.enemyStateDic.Add(CrawlidStateType.Battle.ToString(),new CrawlidBattleState(this, enemyFSM, "Move",this));
    }
    protected override void Start()
    {
        base.Start();
        enemyFSM.InitState(CrawlidStateType.Idle.ToString());
    }

    protected override void Update()
    {
        base.Update();
        enemyFSM.currentState.OnUpdate();
    }
}
