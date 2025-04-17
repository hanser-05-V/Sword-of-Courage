using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownState : PlayerState
{
    public PlayerDownState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
       
    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);
    }

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {

        base.Onupdate(playerInfo, playerStats);
        if(Mathf.Abs(player.rb.velocity.y) > playerInfo.repeatYvelocityMin ) //大于重复播放速度则播放重复动作
        {
            fsm.ChangeState(StateType.DownRepeat);
        }


        if(player.IsGroundDetected()) //检测到地面 变为静止状态
        {
            
            player.SetVecolity(rb.velocity.x, 0);//设置Y速度为0

            if(xInput!=0) //落地瞬间水平方向有输入
            {
                fsm.ChangeState(StateType.Move);
            }
            else//落地瞬间水平方向无输入
            {
                fsm.ChangeState(StateType.DownToGround);
            }
        } 
        if(xInput != 0) //空中水平方向有输入
        {
            player.SetVecolity(playerInfo.moveSpeed * xInput * 0.8f, player.rb.velocity.y);
        }  

        if(player.attackUpAction.triggered) //空中向上攻击
        {
            fsm.ChangeState(StateType.AttackUp);
        }
        if(player.attackDownAction.triggered) //空中向下攻击
        {
       
            fsm.ChangeState(StateType.AttackDown);
        }
    }
}
