using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
        //设置跳跃时长度
        stateTimer = 1f;
        //设置这时候跳跃的水平（和贴墙方向相反）
        //设置水平跳跃高度
        player.SetVelocity(5 * -player.faceDir, player.jumpForce);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
