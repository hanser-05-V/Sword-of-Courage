using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animatorBoolName) : base(_player, _stateMachine, _animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

         if(xInput != 0) 
        {
            Debug.Log("Player is moving");
            stateMachine.ChangeState(player.playerMoveState);
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
