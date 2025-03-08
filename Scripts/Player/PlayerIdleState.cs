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

        //��� ��ɫ��ǽ�����ƶ����ھ�ֹ���˶�����л�
        if (xInput == player.faceDir && player.IsWallDetected())
            return;

        //ˮƽ�������л����ƶ�״̬ ���ҵ�ǰ��ɫ λ���ں�ҡ�׶�
        if (xInput != 0  && !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }
}
