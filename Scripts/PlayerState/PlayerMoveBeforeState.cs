using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBeforeState : PlayerGroundState
{  

    public PlayerMoveBeforeState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {
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
        player.SetVecolity(xInput * playerInfo.moveSpeed, rb.velocity.y); //设置速度
        

        if (xInput == 0) //如果x轴速度为0，则播放空闲动画
        {
            fsm.ChangeState(StateType.Idle);
        }
    }
}
