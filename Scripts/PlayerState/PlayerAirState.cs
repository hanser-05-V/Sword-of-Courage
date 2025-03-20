using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : IState
{
    private PlayerController playerController;

    public PlayerAirState(PlayerController playerController)
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
