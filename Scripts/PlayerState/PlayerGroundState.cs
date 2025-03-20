using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerGroundState : IState // 地面状态 公共方法基类
{
    protected PlayerController playerController;

    public PlayerGroundState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public virtual void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public virtual void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public virtual void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
        if(Input.GetKeyDown(KeyCode.Space) && playerController.IsGroundDetected()) //地面上 按下空格 跳跃
        {
            playerController.ChangeState(StateType.Jump);
        }
        
    }
}
