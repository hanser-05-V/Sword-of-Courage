using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDownReaptState : IState
{

    private PlayerController playerController;

    private string animatorBoolName;

    public PlayerDownReaptState(PlayerController playerController, string animatorBoolName)
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
