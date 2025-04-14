using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerJumpState : PlayerState
{

    private float currentJumpTime;
    private bool isJumping;

    public PlayerJumpState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
        currentJumpTime = 0;
        isJumping = true;
      

    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);
    
      
    }
    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);

        if (isJumping)
        {
            //生成残影
            // playerController.ShowShadow();
        }

        // 如果跳跃时间结束或按键松开，开始下落
        if (isJumping && Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1") || currentJumpTime >= playerInfo.jumpTime)
        {
     
            isJumping = false;    
            fsm.ChangeState(StateType.Air); // 切换到空中状态

        }

        // 如果角色仍在跳跃，增加跳跃时间并调整Y速度
        if (isJumping && (Input.GetKey(KeyCode.Space )|| Input.GetButton("Fire1")))
        {
            currentJumpTime += Time.deltaTime;

            if (currentJumpTime < playerInfo.jumpTime)
            {
                // 使用Sin函数实现跳跃的非线性加速
                float t = currentJumpTime / playerInfo.jumpTime;
                float force = Mathf.Lerp(playerInfo.jumpMinHeight, playerInfo.jumpForce, Mathf.Sin(t * Mathf.PI * 0.5f));
                player.SetVecolity(rb.velocity.x, force);
            }
        }

    }
}
