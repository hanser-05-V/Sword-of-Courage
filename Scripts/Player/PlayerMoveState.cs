using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
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
        //设置移动速度
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);
        //检测到墙壁 直接变为静止
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
