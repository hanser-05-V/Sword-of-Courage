using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerGroundState ,IState
{

    public IdleState(PlayerController playerController) : base(playerController)
    {
        base.playerController = playerController;
    }
    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.Play("playerIdle");
      
    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
 
    }
    

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);
        if(playerController.xInput!= 0)//如果有输入
        {
            playerController.ChangeState(StateType.Move);
        }
    }
}
