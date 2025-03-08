using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
       

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        //检测到墙壁 进入贴墙滑动状态
        if(player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        //检测到地面，进入静止
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        //空中水平移动速度为 平时的80%
        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
    }
}
