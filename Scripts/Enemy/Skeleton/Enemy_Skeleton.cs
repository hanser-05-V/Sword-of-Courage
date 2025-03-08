using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{

    #region ״̬��� State
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }

    public SkeletonDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        //���������Ѿ��������� ״̬��
        base.Awake();
        //��һ��this Ϊ ���˻��ࣨ�洢����������Ϣ������Ϊ�̳��� Enemy,���������滻ԭ�����������ͣ�
        //�ڶ���this Ϊ ָ�����ˣ�����Ϊ ���õ��� �����Ǳ��ű�����¼������Ϣ��
        idleState = new SkeletonIdleState(stateMachine, this, "Idle", this);
        moveState = new SkeletonMoveState(stateMachine, this, "Move", this);
        //ս��״̬�����ʻ��ǳ�����ƶ�
        battleState = new SkeletonBattleState(stateMachine, this, "Move", this);
        attackState = new SkeletonAttackState(stateMachine, this, "Attack", this);
        stunnedState = new SkeletonStunnedState(stateMachine, this, "Stunned", this);

        deadState = new SkeletonDeadState(stateMachine, this, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.InitState(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
    }

    public override bool IsBeStunned()
    {
        if (base.IsBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
