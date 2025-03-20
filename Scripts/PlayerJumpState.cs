using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerJumpState : IState
{

    private PlayerController playerController;

    public PlayerJumpState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        Debug.Log("Player Jump State Enter");
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
      
    }
}
