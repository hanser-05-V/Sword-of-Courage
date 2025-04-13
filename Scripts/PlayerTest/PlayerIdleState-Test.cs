using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState_Test : PlayerState
{
    public PlayerIdleState_Test(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
        Debug.Log("进入静止状态");
    }
    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);
    }
    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);
        if(xInput !=0)
        {
            fsm.ChangeState(StateType.Move);
        }
    }
}
