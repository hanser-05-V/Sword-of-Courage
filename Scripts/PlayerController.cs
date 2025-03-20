using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : Entity //玩家类型协助者 
{
    [SerializeField] private FSM fsm;
    [SerializeField] private UnityAnimationSystem _animationSystem;  // 使用接口
    

    #region 动画相关方法
    public void Play(string animationName) //播放动画接口
    {
        _animationSystem.Play(animationName);
    }
    public void SetBool(string parameterName, bool value) //设置bool参数
    {
        _animationSystem.SetBool(parameterName, value);
    }

    public void SetFolat(string parameterName, float value) //设置float参数
    {
        _animationSystem.SetFloat(parameterName, value);
    }
    public void ChangeState(StateType stateType) // 切换状态方法
    {
        fsm.ChangeState(stateType);
    }
    
    #endregion

  

    
    void Update()
    {
        
    }
    
}