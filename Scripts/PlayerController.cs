using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Vector3> dashPathPosition = new List<Vector3>(); // 用于存储冲刺路径的位置
    [SerializeField] private Transform shadowParent; // 用于存储残影的父物体
    [SerializeField] private int shadowCount = 5; // 用于控制残影的数量
    [SerializeField] private float shadowInterval = 0.15f; // 用于控制残影生成的间隔
    [SerializeField] private float shadowAplhaDuration = 0.2f; // 用于控制残影持续时间
    [SerializeField] private float smoothSpeed =5f; // 用于控制残影平滑速度
    [SerializeField] private float offset = 1f; // 用于设置残影的垂直偏移量
    private float shadowTimer; // 用于控制残影生成的计时器

    private Vector3 startPos; // 用于记录角色起始位置
    private Vector3 endPos; // 用于记录角色结束位置
    #endregion
    
    protected override void Start()
    {
        base.Start();
        //开始时候生成 sprite 列表 到列表
        for(int i=0; i<shadowCount;i++)
        {
            SpriteRenderer shadow =  new GameObject("Shadow" + i).AddComponent<SpriteRenderer>();     
            shadow.transform.SetParent(shadowParent);
            shadow.transform.localScale = Vector3.one;
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

    #region  冲刺相关
    public IEnumerator StartDash() // 冲刺协程
    {
        if (CanDash())
        {
            isDashing = true;
            startPos = targetObject.position; // 记录起始位置 方便残影生成
            // 如果可以冲刺，则开始冲刺协程
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facing;
            }
            ChangeState(StateType.Dash); // 进入冲刺状态
        
            yield return new WaitForSeconds(dashDuration);

            endPos = targetObject.position; // 记录结束位置 方便残影生成
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
    
    #endregion
    #region  残影生成相关
    private void CreateShadow(Vector3 posion)// 创建单个残影
    {
        SpriteRenderer dashSR = GetAvailableShadow();
        dashSR.gameObject.SetActive(true);
        dashSR.transform.position = Vector3.Lerp(startPos,endPos,smoothSpeed* Time.deltaTime );
        if (dashDir == 1)
        {
            dashSR.transform.localRotation = Quaternion.identity; // 朝右
        }
        else
        {
            dashSR.transform.localRotation = Quaternion.Euler(0, 180, 0); // 朝左
        }
        dashSR.sprite = sp.sprite;
        dashSR.color = new Color(dashSR.color.r, dashSR.color.g, dashSR.color.b, 1); // 重置透明度

        // 启动淡出动画（持续shadowLifetime秒后消失）
        dashSR.DOFade(0, shadowAplhaDuration)
            .OnComplete(() => dashSR.gameObject.SetActive(false));
     }
    private SpriteRenderer GetAvailableShadow() //残影缓存池
    {
        foreach (var sr in shadowList)
        {
            if (!sr.gameObject.activeSelf) return sr;
        }
        // 池子不足时扩容
        for (int i = 0; i < 5; i++)
        {
            GameObject newObj = new GameObject("Shadow");
            newObj.AddComponent<SpriteRenderer>();
            shadowList.Add(newObj.GetComponent<SpriteRenderer>());
        }
        return shadowList.Last();
    }
    IEnumerator DashRoutine()
    {
        // 清空旧路径数据
        dashPathPosition.Clear();
    
        // 记录冲刺路径
        while (isDashing)
        {
            int index = 0;
            dashPathPosition.Add(targetObject.position); // 改为记录目标对象的移动轨迹
            CreateShadow(dashPathPosition[index]); //从第一个位置创建残影
            yield return new WaitForSeconds(shadowInterval);
            index++;
            if (index > dashPathPosition.Count)
            {
                index = 0;
            }
        }
    }

    public void StartDashCoroutine() // 启动冲刺协程
    {
        StartCoroutine(DashRoutine());
    } 
    #endregion
}