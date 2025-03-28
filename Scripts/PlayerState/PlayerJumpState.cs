using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerJumpState : IState
{

   private PlayerController playerController;
    private float currentJumpTime;
    private bool isJumping;
    private string animationName;

    public PlayerJumpState(PlayerController playerController , string  animationName)
    {
        this.playerController = playerController;
        this.animationName = animationName;
    }

    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        currentJumpTime = 0;
        isJumping = true;
        // 进入跳跃状态时播放跳跃动画
        playerController.SetBool(animationName, true);
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        // 退出跳跃状态时做一些清理工作（如果有需要）
        playerController.SetBool(animationName, false);
    }
    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
       

        // 如果跳跃时间结束或按键松开，开始下落
        if (isJumping && (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1") || currentJumpTime >= playerInfo.jumpTime))
        {
     
            isJumping = false;    
            playerController.ChangeState(StateType.Air); // 切换到空中状态

        }

        // 如果角色仍在跳跃，增加跳跃时间并调整Y速度
        if (isJumping && (Input.GetKey(KeyCode.Space)||Input.GetButton("Fire1")) )
        {
            currentJumpTime += Time.deltaTime;

            if (currentJumpTime < playerInfo.jumpTime)
            {
                // 使用Sin函数实现跳跃的非线性加速
                float t = currentJumpTime / playerInfo.jumpTime;
                float force = Mathf.Lerp(playerInfo.jumpMinHeight, playerInfo.jumpForce, Mathf.Sin(t * Mathf.PI * 0.5f));
                playerController.SetVecolity(playerController.rb.velocity.x, force);
            }
        }

    }
}
