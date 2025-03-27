using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : IState //玩家空中状态
{
    protected PlayerController playerController;

    public PlayerAirState(PlayerController playerController)
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
        // playerController.SetFloat("Yvelocity",playerController.rb.velocity.y);
        // playerController.SetFolat("Yvelocity",playerController.GetYVelocity());
        if(playerController.IsGroundDetected()) //检测到地面 变为静止状态
        {
            playerController.ChangeState(StateType.Idle);
        }

        if(playerController.xInput != 0) //空中水平方向有输入
        {
            // playerController.SetVelocity(playerInfo.moveSpeed * playerController.xInput * 0.8f, playerController.GetYVelocity()); //空中速度是原来80% 

            playerController.SetVecolity(playerInfo.moveSpeed * playerController.xInput * 0.8f, playerController.rb.velocity.y);
        }
    }

   
}
