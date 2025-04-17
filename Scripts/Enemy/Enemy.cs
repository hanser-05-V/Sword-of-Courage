using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{   

    [Header("移动相关")]
    public float moveSpeed ;
    public float defaultSpeed;

    [Header("攻击检测相关")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform attackCheckPos;
    public float attackDIstance;
    
    [Header("击退相关")]
    [SerializeField] private float hitSpeed;
    [SerializeField] Vector2 hitDirction;
    public bool isHit;

    public EnemyFSM enemyFSM { get; private set; }
    public EnemyStateType enemyStateType;
    protected override void  Awake()
    {
        base.Awake();
        enemyFSM = new EnemyFSM(); //初始化敌人状态机
    }
    protected override void  Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

   

    //击退效果
    public void GetHit(float _hitTime)
    {
        StartCoroutine(Hit(_hitTime));
    }
    IEnumerator Hit(float hitTime)
    {
        isHit = true;
        
        //TODO: 实现击退效果 反向
        rb.velocity = new Vector2(-facing * hitSpeed * hitDirction.x, hitSpeed * hitDirction.y);

        yield return new WaitForSeconds(hitTime);

        isHit = false;

    }

}
