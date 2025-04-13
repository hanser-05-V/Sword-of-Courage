using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.StartDashCoroutine(player);

    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        

    }
    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        // 启动冲刺并设置冲刺方向
        player.SetVecolity(playerInfo.dashSpeed * playerController.dashDir, 0f);
        
        if(!playerController.isDashing) //冲刺结束
        { 
        
            if(player.IsGroundDetected())
            {
               fsm.ChangeState(StateType.Idle); 

            }
            else
            {
                rb.velocity = new Vector2(playerInfo.airDashOverXvelocity * player.facing, -playerInfo.airDashOverYvelocity);
                fsm.ChangeState(StateType.Down);
            }
        }

    }
}
