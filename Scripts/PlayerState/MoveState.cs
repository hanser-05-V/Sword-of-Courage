using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class MoveState : PlayerGroundState, IState
{

    public MoveState(PlayerController playerController) : base(playerController)
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
        Debug.Log("退出移动状态");
    }

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);
        playerController.ChangeXvelocity(playerController.xInput * playerInfo.moveSpeed); //设置x轴速度

        if (playerController.xInput == 0) //如果x轴速度为0，则播放空闲动画
        {
            playerController.ChangeState(StateType.Idle);
        }
        if(Input.GetKeyDown(KeyCode.Space)) //跳跃 切换状态
        {
            playerController.ChangeState(StateType.Jump);
        }
    }
}
