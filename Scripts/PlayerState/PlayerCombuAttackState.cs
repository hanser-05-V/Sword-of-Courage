using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombuAttackState : IState
{

    private PlayerController playerController;
    private String  animatorBoolName;
    public  int attackConter {get; private set;}
    
    protected float attackDic; //攻击的方向
    protected float attackCool = 1f; //两段攻击的最小时间间隔
    protected float lastAttackedTimer; //上一次攻击的时间

    private float  stateTimer ; //玩家惯性保持时间
    public PlayerCombuAttackState(PlayerController playerController, String animatorBoolName)
    {
        this.playerController = playerController;
    }
    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, true);
        
    
        if(attackConter >2 || Time.time >= lastAttackedTimer + attackCool)
        {
            attackConter = 1 ; //重置攻击计数
        } 
        playerController.SetInt("AttackCounter", attackConter);

        stateTimer =  0.1f; //设置持续时间 0.1秒
        attackDic = playerController.facing; //默认方向为面朝向

        if(playerController.xInput !=0)
        {
            attackDic = playerController.xInput; //根据水平方向输入值设置方向
        }
        
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, false);

        attackConter++; //攻击计数加1
        lastAttackedTimer = Time.time; //记录上一次攻击的时间

    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        stateTimer-=Time.deltaTime; //逐帧减少持续时间 进入攻击状态重置

        
        if(stateTimer <= 0 )
        {
            playerController.SetZeroVecolity(); //进入攻击状态后，清除速度
        }

    }
}
