using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBackState : PlayerState
{

    public PlayerJumpBackState(Player _player, PlayerStateMachine _stateMachine, string _animatorBoolName) : base(_player, _stateMachine, _animatorBoolName)
    {
    }


    public override void OnEntry()
    {
        base.OnEntry();

        rb.AddForce(Vector2.up *player.jumpForce);
      
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
