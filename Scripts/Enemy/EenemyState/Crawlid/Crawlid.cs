using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrawlidState
{
    Idle,
    Move,

}
public class Crawlid : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        enemyFSM.enemyStateDic.Add(CrawlidState.Move.ToString(),new CrawlidMoveState(this, enemyFSM, "Move",this));//第一个this是父类Enenmy，第二个this是自身

    }
}
