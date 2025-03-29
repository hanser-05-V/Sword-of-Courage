using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombuAttackState : IState
{

    private PlayerController playerController;
    private String  animatorBoolName;

    public PlayerCombuAttackState(PlayerController playerController, String animatorBoolName)
    {
        this.playerController = playerController;
        
    }
    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, true);
        Debug.Log("Attack");
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, false);
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }
}
