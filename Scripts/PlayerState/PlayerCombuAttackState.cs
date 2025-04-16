using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombuAttackState : PlayerState
{

  
    public  int attackConter {get; private set;}
    
    protected float attackDic; //攻击的方向
    protected float attackCool = 1f; //两段攻击的最小时间间隔
    protected float lastAttackedTimer; //上一次攻击的时间
    public PlayerCombuAttackState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {

    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
        
        triggerCalled = false;
    
        if(attackConter >1 || Time.time >= lastAttackedTimer + attackCool)
        {

            attackConter = 0 ; //重置攻击计数
        } 
        player.animator.SetInteger("AttackCounter", attackConter); //显示攻击计数
     
        stateTimer =  0.1f; //设置持续时间 0.1秒
        attackDic = player.facing; //默认方向为面朝向

        if(xInput !=0)
        {
            attackDic = xInput; //根据水平方向输入值设置方向
        }    
    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);

        attackConter++; //攻击计数加1
        lastAttackedTimer = Time.time; //记录上一次攻击的时间

    }

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);
        stateTimer-=Time.deltaTime; //逐帧减少持续时间 进入攻击状态重置
        if(stateTimer <= 0 )
        {
            player.SetZeroVecolity(); //进入攻击状态后，清除速度
        }
        
        if(triggerCalled) //攻击结束
        {
            fsm.ChangeState(StateType.Idle);
        }
    }
}
