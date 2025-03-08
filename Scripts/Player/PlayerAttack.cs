using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    public int attackCounter {  get; private set; }
    //����ʱ�̵ķ���
    private float attackDic;
    //��ʾ���ι���֮����������ʱ����
    protected float attckCool = 1f;
    //��¼�ϴι���ʱ�̵�ʱ�䣨Time.time ȫ��ʱ�����ʷ���գ�
    protected float lastTimeAttackedTime;
    public PlayerAttack(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
        triggerCalled = false;

        xInput = 0; // ����ʹ�� ���Եõ���xInput������Ϣ��ֵ������ʱ���������

        //��������2 ���� ��ǰʱ�� ���� �ϴι���ʱ�������ȴʱ��
        //ʹ��ȫ��ʱ��Time.Time���õ��ۼ���ʱ��� 
        if (attackCounter > 2 || Time.time>= lastTimeAttackedTime + attckCool)
        {
            attackCounter = 0;
        }
        player.animator.SetInteger("attackCounter", attackCounter);
        //��ƹ����ӳ�ʱ��
        stateTimer = 0.1f;

        //Ĭ�Ϲ����������������
        attackDic = player.faceDir;

        if(xInput !=0)//����������������ı䷽��
            attackDic = xInput;
        //���ÿ����������ʱ����ٶȣ�����ӳ�ʱ�䣬������λ��
        //���Ӵ�ʱ�Ĺ�������������
        player.SetVelocity(player.AttackMoment[attackCounter] * attackDic, rb.velocity.y);
    }

    public override void OnExit()
    {
        base.OnExit();
        //������������
        attackCounter++;
        //��¼�ϴι�����˲ʱʱ��
        lastTimeAttackedTime = Time.time;
        //����Э�̣�������״̬�ȴ���ǰ������ҡ������ִ��
        player.StartCoroutine("BusyFor", 0.15f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //ÿ��״̬�� stateTime��ʼֵΪ0 ���һ�ݼ�
        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
