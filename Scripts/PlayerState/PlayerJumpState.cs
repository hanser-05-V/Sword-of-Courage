using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerJumpState : IState
{
    
    private PlayerController playerController;
    private float currentJumpTime;
    private bool isJumping;
    private float jumpDuration;
    public PlayerJumpState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public  void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        this.jumpDuration = playerInfo.jumpDuration;
        currentJumpTime = 0;
        isJumping = true;
        // playerController.ChangeYvelocity(0);
        playerController.Play("JumpToDown");
       
    }

    public  void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public  void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetFolat("Yvelocity",playerController.GetYVelocity());//让动画根据Y速度对应混合树播放 跳跃/下落动画
        
        playerInfo.jumpDuration -= Time.deltaTime;
        if(playerInfo.jumpDuration<=0)
        {
            Debug.Log("jump over");
            //开始下落
            playerController.ChangeYvelocity(0);
            playerController.ChangeState(StateType.Air);
            playerInfo.jumpDuration = jumpDuration;
            Debug.Log(jumpDuration);
        }
        if(isJumping && Input.GetKey(KeyCode.Space))
        {
            currentJumpTime += Time.deltaTime;//增加跳跃时间
        

            if(currentJumpTime < playerInfo.jumpTime)//跳跃时间未到最大值
            {
                // float force = Mathf.Lerp(playerInfo.jumpMinHeight, playerInfo.jumpForce, currentJumpTime / playerInfo.jumpTime);
                // playerController.ChangeYvelocity(force); //增加跳跃力

                //使用非线性函数
                float t = currentJumpTime / playerInfo.jumpTime;
                float force = Mathf.Lerp(playerInfo.jumpMinHeight,playerInfo.jumpForce,Mathf.Sin(t*Mathf.PI* 0.5f)   );
                playerController.ChangeYvelocity(force); //增加跳跃力
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) || currentJumpTime >= playerInfo.jumpTime)
        {
            isJumping = false;
        } 
        if(playerController.GetYVelocity()<=0)
        {
            playerController.ChangeState(StateType.Air);
        }

    }
}
