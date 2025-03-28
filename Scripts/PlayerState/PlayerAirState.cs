using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : IState //玩家空中状态
{
    protected PlayerController playerController;
    private string animatorBoolName;

    public PlayerAirState(PlayerController playerController,string animatorBoolName)
    {
        this.playerController = playerController;
        this.animatorBoolName = animatorBoolName;
    }
  
    public virtual void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, true);

    }

    public virtual void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.SetBool(animatorBoolName, false);
    }

    public virtual void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        

        if(playerController.xInput != 0) //空中水平方向有输入
        {
            playerController.SetVecolity(playerInfo.moveSpeed * playerController.xInput * 0.8f, playerController.rb.velocity.y);
        }

        if(Mathf.Abs(playerController.rb.velocity.y) > playerInfo.downYvelocityMin)
        {
            playerController.ChangeState(StateType.Down);
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
        playerController.SetFloat("Yvelocity",playerController.rb.velocity.y);
    }

   
}
