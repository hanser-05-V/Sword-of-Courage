using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : Entity //玩家类型协助者 
{
  
    #region 玩家特殊行为变量
    [Header("冲刺相关")]
    public bool isDashing; // 用于判断是否正在冲刺
    private float dashCoolTimer; // 用于计算冲刺的冷却时间
    public float dashDuration = 0.2f; // 冲刺的持续时间（可以根据需要修改）
    public float dashCooldown = 1f; // 冲刺的冷却时间

    public float dashDir ;

    #endregion

    #region 动画相关方法
    // public void Play(string animationName) //播放动画接口
    // {
    //     _animationSystem.Play(animationName);
    // }
    // public void SetBool(string parameterName, bool value) //设置bool参数
    // {
    //     _animationSystem.SetBool(parameterName, value);
    // }

    // public void SetFolat(string parameterName, float value) //设置float参数
    // {
    //     _animationSystem.SetFloat(parameterName, value);
    // }
    // public void ChangeState(StateType stateType) // 切换状态方法
    // {
    //     fsm.ChangeState(stateType);
    // }
    // public AnimatorStateInfo GetCurAnimStateInfo() //获取当前动画状态信息
    // {
    //     return _animationSystem.GetCurAnimStateInfo();
    // }

    #endregion
    
    protected override void Update()
    {
        base.Update();
        //所有状态 公共行为
        dashCoolTimer -= Time.deltaTime; // 冲刺冷却时间减少
        if(Input.GetKeyDown(KeyCode.Q) )
        {
            StartCoroutine(StartDash());// 冲刺
        }

    }
    public IEnumerator StartDash()
    {
        if (CanDash())
        {
            // 如果可以冲刺，则开始冲刺协程
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facing;
            }
            ChangeState(StateType.Dash); // 进入冲刺状态
        
            yield return new WaitForSeconds(dashDuration);

            ChangeState(StateType.Idle); // 回到空闲状态
        }
    }
    private bool CanDash()
    {
        if(dashCoolTimer < 0 )
        {
            dashCoolTimer =  dashCooldown;
            return true;
        }
        Debug.Log("技能正在冷却");
        return false;
    }
}