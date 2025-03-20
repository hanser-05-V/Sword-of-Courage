using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : IState
{
    private PlayerController playerController;

    public PlayerGroundState(PlayerController playerController)
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
