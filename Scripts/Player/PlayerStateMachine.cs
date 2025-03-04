using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState {get;private set;}

    public void InitState(PlayerState startState)
    {
        //TODO 更新状态 进入新状态
    }

    public void ChangeState(PlayerState newState)
    {
        //TODO 退出上一次状态 更新状态 进入新状态
    }
}
