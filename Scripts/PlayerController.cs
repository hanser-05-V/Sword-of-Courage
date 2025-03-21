using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : Entity //玩家类型协助者 
{
    [SerializeField] private FSM fsm;
    [SerializeField] private UnityAnimationSystem _animationSystem;  // 使用接口
    

    #region 玩家特殊行为变量

    [Header("冲刺相关")]
    public float dashSpeed;
    public float dashDruation;
    [SerializeField] private float defalutDashDruation;
    [SerializeField] private float dashCooldown;

    public float dashDir {get; private set;}
    [SerializeField] private float dashTimer;    
    private bool isDashing;
    
    #endregion

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
    
    protected override void Update()
    {
        base.Update();
        //所有状态 公共行为
       dashTimer -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing )
        {
            StartCoroutine(StartDash());// 冲刺
        }

    }
    IEnumerator StartDash() //冲刺协程
    {
        dashDruation = defalutDashDruation;
        if(CanDash())
        {
            isDashing = true;
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir == 0)
            {
                dashDir = facing;
            }
            ChangeState(StateType.Dash);
            yield return new WaitForSeconds(dashDruation);
            dashDruation = 0;
            Debug.Log("冲刺结束");
        }
        else
        {
            yield return null;
        }
        isDashing = false;
    }
    
    private bool  CanDash() //判断能否冲刺
    {
     
        if(dashCooldown > dashTimer)
        {
            dashTimer = 3f;
            Debug.Log("Can Dash");
            return true;
        }
        return false;
    }
}