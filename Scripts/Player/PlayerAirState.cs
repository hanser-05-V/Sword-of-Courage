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
        //��⵽ǽ�� ������ǽ����״̬
        if(player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        //��⵽���棬���뾲ֹ
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        //����ˮƽ�ƶ��ٶ�Ϊ ƽʱ��80%
        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
    }
}
