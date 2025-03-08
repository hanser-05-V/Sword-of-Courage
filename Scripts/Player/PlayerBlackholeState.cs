using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime = 0.4f;
    private bool skillUsed;//�����Ƿ�ʹ��
    private float defaultGravity;
        

    public PlayerBlackholeState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();

        defaultGravity = rb.gravityScale;
        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
       
    }

    public override void OnExit()
    {
        base.OnExit();
        rb.gravityScale = defaultGravity;
        player.fx.MakeTransprent(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 15); // �ɿ�
        }
        if(stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -0.1f);// ��������

            if(!skillUsed) 
            {
                if(player.skill.blackhole.CanUseSkill())
                    skillUsed = true;
            }
        }
        if (player.skill.blackhole.BlackholeFinished())
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
