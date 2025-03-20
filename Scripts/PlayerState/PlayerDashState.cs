using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : IState
{

    private PlayerController playerController;

    public PlayerDashState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }
}
