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
        // 如果冲刺持续时间结束，切换到Idle状态
        if ( playerController.dashDuration <= 0)
        { 
            Debug.Log("dash end");
            playerController.ChangeState(StateType.Idle);
        }
           
    }
}
