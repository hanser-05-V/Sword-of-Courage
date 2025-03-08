using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{

    #region 状态相关 State
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }

    public SkeletonDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        //基类里面已经申明好了 状态机
        base.Awake();
        //第一个this 为 敌人基类（存储公共敌人信息）（因为继承了 Enemy,根据里氏替换原则来传递类型）
        //第二个this 为 指定敌人（这里为 骷髅敌人 ，就是本脚本，记录特殊信息）
        idleState = new SkeletonIdleState(stateMachine, this, "Idle", this);
        moveState = new SkeletonMoveState(stateMachine, this, "Move", this);
        //战斗状态，本质还是朝玩家移动
        battleState = new SkeletonBattleState(stateMachine, this, "Move", this);
        attackState = new SkeletonAttackState(stateMachine, this, "Attack", this);
        stunnedState = new SkeletonStunnedState(stateMachine, this, "Stunned", this);

        deadState = new SkeletonDeadState(stateMachine, this, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.InitState(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
    }

    public override bool IsBeStunned()
    {
        if (base.IsBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
