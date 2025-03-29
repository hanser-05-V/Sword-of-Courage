using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerDashState : IState
{

    private PlayerController playerController;
    private string animatorBoolName;


    public PlayerDashState(PlayerController playerController, string animatorBoolName)
    {
        this.playerController = playerController;
        this.animatorBoolName = animatorBoolName;
    }

    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, true);

    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, false);

    }
    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        // 启动冲刺并设置冲刺方向
         playerController.SetVecolity(playerInfo.dashSpeed * playerController.dashDir, 0f);
         
        if(!playerController.isDashing) //冲刺结束
        { 
        

            if(playerController.IsGroundDetected())
            {
                playerController.ChangeState(StateType.Idle);

            }
            else
            {
                playerController.rb.velocity = new Vector2(playerInfo.airDashOverXvelocity * playerController.facing, -playerInfo.airDashOverYvelocity);
                playerController.ChangeState(StateType.Down);
            }
        }

    }
}
