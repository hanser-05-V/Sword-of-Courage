using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;

    protected Enemy enemyBase;

    protected string animatorBoolName;

    protected float stateTime;

    protected Rigidbody2D rb;

    //ÅÐ¶Ï¹¥»÷ÊÇ·ñ½áÊø
    public bool triggerCalled;
    public EnemyState(EnemyStateMachine stateMachine, Enemy enemyBase, string animatorBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemyBase = enemyBase;
        this.animatorBoolName = animatorBoolName;
    }

    public virtual void OnUpdate()
    {
        stateTime -= Time.deltaTime;
    }

    public virtual void OnEntry()
    {
        triggerCalled = false;
        enemyBase.animator.SetBool(animatorBoolName, true);
        rb = enemyBase.rb;
    }
    public virtual void OnExit()
    {
        enemyBase.animator.SetBool(animatorBoolName,false);
        triggerCalled = false;
    }
}
