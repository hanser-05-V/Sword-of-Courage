using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{

    private PlayerController playerController;

    public MoveState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        Debug.Log("进入移动状态");
        playerController.Play("playerMove");
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }
}
