using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState {  get; private set; }

    public void InitState(PlayerState startState)
    {
        currentState = startState;
        currentState.OnEntry();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEntry();
    }
}
