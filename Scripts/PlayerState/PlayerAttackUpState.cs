using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackUpState : PlayerState
{
    public PlayerAttackUpState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
        Debug.Log("进入向上攻击状态");

        triggerCalled = false;
    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);
    }

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);


        if(triggerCalled)//攻击结束
        {
            fsm.ChangeState(StateType.Idle);
        }
    }
}
