using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour 
{
     [SerializeField] private FSM fsm;
     [SerializeField] private UnityAnimationSystem _animationSystem;  // 使用接口
    
     [SerializeField] private UnityPhysicsSystem physicsSystem; // 使用接口
    

    #region 动画相关方法
    public void Play(string animationName) //播放动画接口
    {
        _animationSystem.Play(animationName);
    }
    public void ChangeState(StateType stateType) // 切换状态方法
    {
        fsm.ChangeState(stateType);
    }

    #endregion

    #region 物理相关方法

    public void ApplyJumpForece() //添加跳跃力
    {
        physicsSystem.ApplyJumpForece();
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
    #endregion


    public float xInput;
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        Debug.Log(xInput);
    }
  
}