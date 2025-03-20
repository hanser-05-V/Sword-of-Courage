using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : Entity //玩家类型协助者 
{
    [SerializeField] private FSM fsm;
    [SerializeField] private UnityAnimationSystem _animationSystem;  // 使用接口
    
    [SerializeField] private UnityPhysicsSystem physicsSystem; // 使用接口

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

    #region 物理相关方法

    public void ApplyJumpForece(float jumpForce,ForceMode2D forceMode) //添加跳跃力
    {
        physicsSystem.ApplyJumpForece(jumpForce,forceMode);
    }
    public bool IsGrounded() // 是否在地面
    {
        return physicsSystem.IsGrounded();
    }
    public void SetVelocity(float xVelocity, float yVelocity) //设置速度
    {
        physicsSystem.SetVelocity(xVelocity, yVelocity);
    }
    public void SetZeroVelocity()//设置速度为0
    {
        physicsSystem.SetZeroVelocity();
    }

    public void ChangeXvelocity(float xVelocity) //设置x轴速度
    {
        physicsSystem.ChangeXvelocity(xVelocity);
    }
    public void ChangeYvelocity(float yVelocity) //设置y轴速度
    {
        physicsSystem.ChangeYvelocity(yVelocity);
    }

    public Rigidbody2D GetRigidbody2D() //获取刚体
    {
        return physicsSystem.GetRigidbody2D();
    }

    public float GetYVelocity() //获取y轴速度
    {
        return physicsSystem.GetYVelocity();
    }

    #endregion
 
    
}