using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerGroundState : PlayerState
{


    private bool isJumping = false;

    public PlayerGroundState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
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
        
        if(Input.GetKeyDown(KeyCode.Space )&& player.IsGroundDetected()) //地面上 按下空格 跳跃
        {
            isJumping = true;
            // playerController.ShowShadow();
            // playerController.ChangeYvelocity(0); // 重置y轴速度
            player.SetVecolity(rb.velocity.x,0); //重置Y轴速度
            fsm.ChangeState(StateType.Jump);
        }
      
        if(!player.IsGroundDetected()) //没有检测到地面 切换到空中状态 （空中冲刺）
        {
            fsm.ChangeState(StateType.Air);
        }

        if(Input.GetMouseButtonDown(0))
        {
            fsm.ChangeState(StateType.Attack);
        }
        
    }
}
