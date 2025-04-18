using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : IEnemyState
{
    protected EnemyFSM fsm;
    protected Enemy enemyBase;
    protected string animatorBoolName;

    protected float stateTime;

    protected bool triggerCalled; //判断攻击是否结束

    protected Rigidbody2D rb;

    public EnemyState(Enemy _enemyBase, EnemyFSM _fsm, string _animatorBoolName)
    {
        this.enemyBase = _enemyBase;
        this.fsm = _fsm;
        this.animatorBoolName = _animatorBoolName;
    }
    
    public virtual void OnEnter()
    {
        
        triggerCalled = false; 
        rb = enemyBase.rb;

        enemyBase.animator.SetBool(animatorBoolName, true);

    }

    public virtual void OnExit()
    {
       enemyBase.animator.SetBool(animatorBoolName, false);
       triggerCalled = false;
    }

    public virtual void OnUpdate()
    {
        stateTime -= Time.deltaTime; //状态机计时结束  
    }
}
