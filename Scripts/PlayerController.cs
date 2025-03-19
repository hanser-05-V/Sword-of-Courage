using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private FSM fsm;
     [SerializeField] private IAnimationSystem _animationSystem;  // 使用接口

    public void Play(string animationName) //播放动画接口
    {
        _animationSystem.Play(animationName);
    }
    public void ChangeState(StateType stateType) // 切换状态方法
    {
        fsm.ChangeState(stateType);
    }
}
public class UnityAnimationSystem : IAnimationSystem 
{
    private readonly Animator _animator;

    public UnityAnimationSystem(Animator animator) 
    {
        _animator = animator;
    }

    public void Play(string animationName) //播放动画接口
    {

        _animator.Play(animationName);  

    }  

    public void SetBool(string parameterName, bool value) //设置参数接口
    {
        _animator.SetBool(parameterName, value);
    }
}