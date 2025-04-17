using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : IEnemyState
{
    protected EnemyFSM fsm;
    protected Enemy enemyBase;
    protected string animatorBoolName;

    public EnemyState(Enemy _enemyBase, EnemyFSM _fsm, string _animatorBoolName)
    {
        this.enemyBase = _enemyBase;
        this.fsm = _fsm;
        this.animatorBoolName = _animatorBoolName;
    }
    
    public virtual void OnEnter()
    {
        
    }

    public virtual void OnExit()
    {
       
    }

    public virtual void OnUpdate()
    {
        
    }
}
