using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownToGroundState : PlayerGroundState
{

    public PlayerDownToGroundState(PlayerController playerController, string animatorBoolName) : base(playerController, animatorBoolName)
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

        // if(playerController.xInput != 0)
        // {
        //     playerController.ChangeState(StateType.Move);
        // }
    }
}
