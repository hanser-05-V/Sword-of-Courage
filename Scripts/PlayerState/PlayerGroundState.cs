using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerGroundState : IState // 地面状态 公共方法基类
{
    protected PlayerController playerController;

    private bool isJumping = false;
    private string animatorBoolName; // 动画bool参数名

    public PlayerGroundState(PlayerController playerController,string animatorBoolName)
    {
        this.playerController = playerController;
        this.animatorBoolName = animatorBoolName;
    }
    public virtual void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, true); // 切换动画bool参数为true
    }

    public virtual void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, false); // 切换动画bool参数为false
    }

    public virtual void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1") )&& playerController.IsGroundDetected()) //地面上 按下空格 跳跃
        {
            isJumping = true;
            // playerController.ChangeYvelocity(0); // 重置y轴速度
            playerController.SetVecolity(playerController.rb.velocity.x,0); //重置Y轴速度
            playerController.ChangeState(StateType.Jump);
        }
      
        if(!playerController.IsGroundDetected()) //没有检测到地面 切换到空中状态 （空中冲刺）
        {
       
            playerController.ChangeState(StateType.Air);
        }
    }
}
