using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownState : IState
{

    private PlayerController playerController;
    private String animatorBoolName;

    public PlayerDownState(PlayerController playerController, String animatorBoolName)
    {
        this.playerController = playerController;
        this.animatorBoolName = animatorBoolName;
    }

    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, true);
       
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, false);
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {

        if(Mathf.Abs(playerController.rb.velocity.y) > playerInfo.repeatYvelocityMin ) //大于重复播放速度则播放重复动作
        {
            playerController.ChangeState(StateType.DownRepeat);
        }


        if(playerController.IsGroundDetected()) //检测到地面 变为静止状态
        {
            
            playerController.SetVecolity(playerController.rb.velocity.x, 0);//设置Y速度为0

            if(playerController.xInput!=0) //落地瞬间水平方向有输入
            {
                playerController.ChangeState(StateType.Move);
            }
            else//落地瞬间水平方向无输入
            {
                playerController.ChangeState(StateType.DownToGround);
            }
        } 


        if(playerController.xInput != 0) //空中水平方向有输入
        {
            playerController.SetVecolity(playerInfo.moveSpeed * playerController.xInput * 0.8f, playerController.rb.velocity.y);
        }  
    
        playerController.SetFloat("Yvelocity",playerController.rb.velocity.y);
    }
}
