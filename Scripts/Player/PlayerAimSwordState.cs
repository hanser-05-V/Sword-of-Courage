using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    private Vector2 mousePosition;
    public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
   
        player.skill.sword.DotsActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        //��ҡ Ͷ������ȴ�����ſ��� �ƶ�
        player.StartCoroutine("BusyFor", 0.2f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        player.SetZeroVelocity();
        if (Input.GetMouseButtonUp(1))
        {
            stateMachine.ChangeState(player.idleState);
        }
        mousePosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x > player.transform.position.x && player.faceDir ==-1)
            player.Filp();
        else if (mousePosition.x < player.transform.position.x && player.faceDir==1)
            player.Filp();
    }
}
