using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState, IState
{

    public PlayerMoveState(PlayerController playerController) : base(playerController)
    {
        base.playerController = playerController;
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        Debug.Log("进入移动状态");
        playerController.Play("playerMove");
        // playerController.SetBool("Move",true);
    }

    public  override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
       
    }

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);
        playerController.ChangeXvelocity(playerController.xInput * playerInfo.moveSpeed); //设置x轴速度

        if (playerController.xInput == 0) //如果x轴速度为0，则播放空闲动画
        {
            playerController.ChangeState(StateType.Idle);
        }
        
        
    }
}
