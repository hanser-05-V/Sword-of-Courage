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
        Debug.Log("进入 跳跃状态");

        playerController.Play("JumpToDown");

        playerController.ApplyJumpForece(playerInfo.jumpForce,ForceMode2D.Impulse);
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetFolat("Yvelocity",playerController.GetYVelocity());
        Debug.Log(playerController.GetYVelocity());
    }
}
