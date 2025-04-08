using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
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
    
    [Header ("残影生成相关")]
    private List<SpriteRenderer> shadowList = new List<SpriteRenderer>(); // 用于存储残影的列表
    [SerializeField] private Transform shadowParent; // 用于存储残影的父物体
    [SerializeField] private int shadowCount = 5; // 用于控制残影的数量
    [SerializeField] private float shadowInterval = 0.5f; // 用于控制残影生成的间隔
    [SerializeField] private float shadowDuration = 0.5f; // 用于控制残影持续时间
    [SerializeField] private float changeSpeed; // 用于控制残影透明度变化速度
    [SerializeField] private float verticalOffset = 1f; // 用于设置残影的垂直偏移量
    private float shadowTimer; // 用于控制残影生成的计时器
    #endregion
    
    
    
    protected override void Start()
    {
        base.Start();
        //开始时候生成 sprite 列表 到列表
        for(int i=0; i<shadowCount;i++)
        {
            SpriteRenderer shadow =  new GameObject("Shadow" + i).AddComponent<SpriteRenderer>();     
            shadow.transform.SetParent(shadowParent);
            shadow.gameObject.SetActive(false); // 隐藏残影
            shadowList.Add(shadow);// 添加到列表
        }

    }

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
    public IEnumerator StartDash() // 冲刺协程
    {
        if (CanDash())
        {
            isDashing = true;
           
            // 如果可以冲刺，则开始冲刺协程
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facing;
            }
            ChangeState(StateType.Dash); // 进入冲刺状态
        
            yield return new WaitForSeconds(dashDuration);

         
            isDashing = false;
        }
    }
    private bool CanDash() // 判断是否可以冲刺
    {
        if(dashCoolTimer < 0 )
        {
            dashCoolTimer =  dashCooldown;
            return true;
        }
        Debug.Log("技能正在冷却");
        return false;
    }
    public void ShowShadow()
    {
        StartCoroutine(ShowShadowBegin());
    }
    IEnumerator ShowShadowBegin() // 显示残影协程
    {

        for(int i =0 ;i<shadowCount;i++)
        {
             Vector3 offset = Vector3.zero;
            // 根据玩家的垂直速度来判断偏移
            if (rb.velocity.y > 0) // 如果玩家在跳跃
            {
                offset = Vector3.down * verticalOffset; // 跳跃时残影位于角色下方
            }
            else if (rb.velocity.y < 0) // 如果玩家在下落
            {
                offset = Vector3.up * verticalOffset; // 下落时残影位于角色上方
            }

            shadowList[i].color= Color.white;
            shadowList[i].sprite = sp.sprite; // 复制角色的 sprite
            shadowList[i].transform.position = transform.position;
            shadowList[i].transform.localScale = sp.transform.localScale; // 复制角色的大小
            shadowList[i].transform.localEulerAngles = new Vector3(0,facing<0? 0: 180, 0); // 复制角色的朝向
            shadowList[i].gameObject.SetActive(true); // 显示残影
            //TODO : 添加残影Aplha 变化
            shadowList[i].DOFade(0,shadowDuration);
            yield return new WaitForSeconds(shadowInterval);
           
        }

    }
}