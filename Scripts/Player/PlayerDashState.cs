using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
        ////if(player.canClone)
        //    player.skill.clone.CreatClone(player.transform,Vector2.zero);
        player.skill.clone.CreatCloneOnDashStart();
        stateTimer = player.dashDuration;
    }

    public override void OnExit()
    {
        base.OnExit();
        player.skill.clone.CreatCloneOnDashOver();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        //设置冲刺速度
        player.SetVelocity(player.dashDir * player.dashSpeed,0);
        //冲刺结束回到初始状态
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
        if (player.IsWallDetected() && !player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
    }
}
