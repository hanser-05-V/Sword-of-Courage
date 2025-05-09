using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
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
    // [SerializeField] private float smoothSpeed =5f; // 用于控制残影平滑速度
    [SerializeField] private float offset = 1f; // 用于设置残影的偏移量
 
    private Vector3 startPos; // 用于记录角色起始位置
    private Vector3 endPos; // 用于记录角色结束位置
    #endregion
    void Start()
    {
        CreatShadowBegain(); // 创建残影缓存池
    }
    public void CreatShadowBegain()
    {
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

    #region  冲刺相关
    public IEnumerator StartDash(Player player) // 冲刺协程
    {
        // if (CanDash())
        // {
            isDashing = true;
            startPos = player.transform.position; // 记录起始位置 方便残影生成
            // 如果可以冲刺，则开始冲刺协程
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = player.facing;
            }
            player.playerFSM.ChangeState(StateType.Dash); // 进入冲刺状态
        
            yield return new WaitForSeconds(dashDuration);

            endPos = player.transform.position; // 记录结束位置 方便残影生成
            isDashing = false;
        // }
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
    private void CreateShadow(Vector3 posion,Player player)// 创建单个残影
    {
        float distance = Vector3.Distance(posion,endPos);// 当前位置距离终点的距离
        SpriteRenderer dashSR = GetAvailableShadow();
        dashSR.gameObject.SetActive(true);
        // dashSR.transform.position = Vector3.Lerp(startPos,endPos,smoothSpeed* Time.deltaTime ); //位置插值 生成残影
        dashSR.transform.position = posion; // 直接设置位置
    
        dashSR.sprite = player.sp.sprite;
        dashSR.color = new Color(dashSR.color.r, dashSR.color.g, dashSR.color.b, 1); // 重置透明度

        // 启动淡出动画（持续shadowLifetime秒后消失）
        float dashDuration = Mathf.Clamp(distance /2f,0.1f,shadowAplhaDuration);// 计算残影持续时间
        dashSR.DOFade(0, dashDuration)
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
    IEnumerator DashRoutine(Player player)
    {
        // 清空旧路径数据 
        dashPathPosition.Clear();
        // // 记录冲刺路径
        // while (isDashing)
        // {
        //     int index = 0;
        //     Debug.Log("冲刺路径：" + index);
        //     dashPathPosition.Add(player.transform.position); // 改为记录目标对象的移动轨迹
        //     CreateShadow(dashPathPosition[index],player); //从第一个位置创建残影
        //     yield return new WaitForSeconds(shadowInterval);
        //     index++;
        //     if (index > dashPathPosition.Count)
        //     {
        //         index = 0;
        //     }
        // }

        // 记录玩家的路径点
        Vector3 previousPosition = player.transform.position;
        dashPathPosition.Add(previousPosition);

        while (isDashing)
        {
            // 记录玩家新的位置
            Vector3 currentPosition = player.transform.position;

            // 如果当前位置与上一个位置不同，则添加新的路径点
            if (currentPosition != previousPosition)
            {
                dashPathPosition.Add(currentPosition);
                previousPosition = currentPosition;
            }
        // 创建残影：对历史路径点进行线性插值
        for (int i = 1; i < dashPathPosition.Count; i++)
        {
            // 获取两个相邻点
            Vector3 startPoint = dashPathPosition[i - 1];
            Vector3 endPoint = dashPathPosition[i];

            // 计算插值点的数量，假设每两个路径点之间生成3个残影
            int shadowCount = 1;  // 每对点之间的插值数量
            for (int j = 1; j <= shadowCount; j++)
            {
                float t = j / (float)(shadowCount + 1);  // 线性插值的比例
                Vector3 shadowPosition = Vector3.Lerp(startPoint + new Vector3(offset * dashDir, 0, 0), endPoint+new Vector3(offset * dashDir, 0, 0), t);
                CreateShadow(shadowPosition, player);
            }
        }
            yield return new WaitForSeconds(shadowInterval);
        }
    }
    public void StartDashCoroutine(Player player) // 启动冲刺协程
    {
        player.StartCoroutine(DashRoutine(player));
    } 
    #endregion
}