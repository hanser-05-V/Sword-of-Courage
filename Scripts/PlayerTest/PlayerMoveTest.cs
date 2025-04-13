using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : PlayerState
{
    public PlayerMoveTest(Player_TEst1 player, Player_FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
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
        rb.velocity = new Vector2(playerInfo.moveSpeed * xInput, 0);
        if(xInput ==0)
        {
            fsm.ChangeState(StateType.Idle);
        }
    }
}
