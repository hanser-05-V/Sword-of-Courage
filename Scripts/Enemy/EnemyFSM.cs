using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM 
{
    public EnemyState currentState {get; private set;} 

    public Dictionary<String,EnemyState> enemyStateDic = new Dictionary<String, EnemyState>();

    public void InitState(String begainType)
    {
        currentState = enemyStateDic[begainType];
    
        currentState.OnEnter();
    }

    public void ChangeState(String newType)
    {
        if(currentState!= null)
           currentState.OnExit();
        currentState = enemyStateDic[newType];
        currentState.OnEnter();
    }

}
