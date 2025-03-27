using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerGroundState : IState // 地面状态 公共方法基类
{
    protected PlayerController playerController;

    public float maxJumpHeight = 5f; // 最大跳跃高度
    public float jumpTime = 0.5f; // 跳跃持续时间
    private float currentJumpTime = 0f; // 当前跳跃时间

    private bool isJumping = false;

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
            isJumping = true;
            playerController.ChangeYvelocity(0); // 重置y轴速度
            playerController.ChangeState(StateType.Jump);
        }
      
        if(!playerController.IsGroundDetected()) //没有检测到地面 切换到空中状态 （空中冲刺）
        {
            Debug.Log("切换到空中状态");
            playerController.ChangeState(StateType.Air);
        }
    }
}
