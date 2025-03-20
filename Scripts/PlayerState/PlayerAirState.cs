using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : IState //玩家空中状态
{
    private PlayerController playerController;

    public PlayerAirState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
  
    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
      
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {

        if(playerController.IsGroundDetected()) //检测到地面 变为静止状态
        {
            Debug.Log("空中进入地面");
            playerController.ChangeState(StateType.Idle);
        }

        if(playerController.xInput != 0) //空中水平方向有输入
        {
            playerController.SetVelocity(playerInfo.moveSpeed * playerController.xInput * 0.8f, playerController.GetYVelocity()); //空中速度是原来80% 
        }
    }

   
}
