using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBeforeState : PlayerGroundState,IState
{
    private PlayerController playerController;
    public PlayerMoveBeforeState(PlayerController playerController, string animatorBoolName) : base(playerController, animatorBoolName)
    {
        this.playerController = playerController;
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);
    }

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);
        playerController.SetVecolity(playerController.xInput * playerInfo.moveSpeed, playerController.rb.velocity.y); //设置速度
        

        if (playerController.xInput == 0) //如果x轴速度为0，则播放空闲动画
        {
            playerController.ChangeState(StateType.Idle);
        }
    }
}
