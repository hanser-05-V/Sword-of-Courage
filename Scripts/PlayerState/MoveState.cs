using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class MoveState : IState
{

    private PlayerController playerController;

    public MoveState(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {

        Debug.Log("进入移动状态");
        playerController.Play("playerMove");
        // playerController.SetBool("Move",true);
  
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        Debug.Log("退出移动状态");
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        playerController.ChangeXvelocity(playerController.xInput * playerInfo.moveSpeed); //设置x轴速度

        if (playerController.xInput == 0) //如果x轴速度为0，则播放空闲动画
        {
            playerController.ChangeState(StateType.idle);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerController.ChangeState(StateType.jump);
        }
    }
}
