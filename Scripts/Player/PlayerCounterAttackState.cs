using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    //��¼��ײ����������ײ��
    private Collider2D[] collider2Ds;

    private bool canCreatClone;
    public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        player.SetZeroVelocity ();
        base.OnEntry();
        stateTimer = player.counterTime;
        player.animator.SetBool("SuccessCounterAttack", false);

        canCreatClone = true;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        player.SetZeroVelocity();

        collider2Ds = Physics2D.OverlapCircleAll(player.attackCheckPos.position, player.attackRadius);

        foreach(Collider2D hitInfo in collider2Ds)
        {
            if(hitInfo.GetComponent<Enemy>()!=null)
            {
                if (hitInfo.GetComponent<Enemy>().IsBeStunned())
                {
                    stateTimer = 10;//����һ���ܴ��ֵ��ȷ���ǵ����ɹ����˳�
                    player.animator.SetBool("SuccessCounterAttack",true);

                    if(canCreatClone)
                    {
                        canCreatClone = false;
                        SkillManager.Instance.clone.CreatCloneOnCounterAttack(hitInfo.transform);
                    }
                }
            }
        }
        if(stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
