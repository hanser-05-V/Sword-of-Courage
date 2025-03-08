using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState currentState { get; private set; }

    public void InitState(EnemyState state)
    {
        currentState = state;
        currentState.OnEntry();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEntry();
    }
}
