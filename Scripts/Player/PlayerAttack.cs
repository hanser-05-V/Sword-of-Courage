using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    public int attackCounter {  get; private set; }
    //攻击时刻的方向
    private float attackDic;
    //表示两次攻击之间允许的最短时间间隔
    protected float attckCool = 1f;
    //记录上次攻击时刻的时间（Time.time 全局时间的历史快照）
    protected float lastTimeAttackedTime;
    public PlayerAttack(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
        triggerCalled = false;

        xInput = 0; // 后续使用 可以得到的xInput不是休息的值，进入时候进行重置

        //计数超过2 或者 当前时间 大于 上次攻击时间加上冷却时间
        //使用全局时间Time.Time，得到累计是时间差 
        if (attackCounter > 2 || Time.time>= lastTimeAttackedTime + attckCool)
        {
            attackCounter = 0;
        }
        player.animator.SetInteger("attackCounter", attackCounter);
        //设计攻击延迟时间
        stateTimer = 0.1f;

        //默认攻击方向就是脸朝向
        attackDic = player.faceDir;

        if(xInput !=0)//如果有输入根据输入改变方向
            attackDic = xInput;
        //设计每个攻击动作时候的速度，结合延迟时间，来产生位移
        //更加此时的攻击方向来攻击
        player.SetVelocity(player.AttackMoment[attackCounter] * attackDic, rb.velocity.y);
    }

    public override void OnExit()
    {
        base.OnExit();
        //攻击计数增加
        attackCounter++;
        //记录上次攻击的瞬时时刻
        lastTimeAttackedTime = Time.time;
        //开启协程，让其他状态等待当前攻击后摇结束再执行
        player.StartCoroutine("BusyFor", 0.15f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //每个状态的 stateTime初始值为0 而且会递减
        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
