using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //解决 角色贴墙继续移动，在静止和运动疯狂切换
        if (xInput == player.faceDir && player.IsWallDetected())
            return;

        //水平有输入切换到移动状态 并且当前角色 位处于后摇阶段
        if (xInput != 0  && !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }
}
