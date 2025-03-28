using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : IState
{

    private PlayerController playerController;
    private string animatorBoolName ;

    public PlayerDashState(PlayerController playerController , string animatorBoolName)
    {
        this.playerController = playerController;
        this.animatorBoolName = animatorBoolName;
    }

    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, true);
        playerController.SetVecolity(playerController.dashSpeed * playerController.dashDir,0f);
 
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, false);
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        if(playerController.dashDruation <=0)
        {
            playerController.ChangeState(StateType.Idle);
        }
       
    }
}
