using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState ,IState
{

    public PlayerIdleState(PlayerController playerController,string animatorBoolName) : base(playerController,animatorBoolName)
    {
        // base.playerController = playerController;
    }
    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
        playerController.SetZeroVecolity();
      
    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);
    }


    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);

        if(playerController.xInput!= 0)//如果有输入
        {
            playerController.ChangeState(StateType.MoveBefore);
        }
    }
}
