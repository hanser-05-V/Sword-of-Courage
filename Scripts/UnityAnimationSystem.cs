using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityAnimationSystem :  MonoBehaviour , IAnimationSystem
{
    [SerializeField] private  Animator _animator; //依赖性动画组件

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Play(string animationName) //播放动画接口
    {

        _animator.Play(animationName);  

    }  

    public void SetBool(string parameterName, bool value) //设置bool参数接口
    {
        _animator.SetBool(parameterName, value);
    }

    public void SetFloat(string parameterName, float value) //设置浮点数参数接口
    {
        _animator.SetFloat(parameterName, value);
    }
}
