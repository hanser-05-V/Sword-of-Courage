using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;

public class Enemy : Entity
{

    [Header("�ƶ����")]
    public float moveSpeed;
    public float idleTime;
    public float balletTime;
    private float defaultMoveSpeed;

    [Header("����������")]
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform attackCheck;
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastAttackTime;

    [Header("����ѣ����� Stuned")]
    public float stunnedTime;
    [HideInInspector] public bool isStunned;
    public Vector2 stunnedDir;
    protected bool canbeStunned = false;
    [SerializeField] protected GameObject counterImage;

    protected EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine =new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }
        
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.OnUpdate();
      
    }

    //������ʹ����ײ�õ� player ���� ���з���ֵ���Ǽ򵥵�bool
    public RaycastHit2D isPlayerDeleted()
    {
        float playerDistanceCheck =10;

        RaycastHit2D playerDetected = Physics2D.Raycast(this.transform.position, Vector2.right * faceDir, playerDistanceCheck, whatIsPlayer);

        RaycastHit2D wallDetected = Physics2D.Raycast(this.transform.position, Vector2.right * faceDir, playerDistanceCheck + 1, whatIsGround);

        if (wallDetected)
        {
       
            return default(RaycastHit2D); //��ֹ�����ص� 
        }

        return playerDetected;
    }

    public virtual void FreezeTime(bool _timeForzen)
    {
        if (_timeForzen)
        {
            moveSpeed = 0;
            animator.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            animator.speed = 1;
        }
    }

    public virtual void FreezeTimerFor(float _duration)
    {
        StartCoroutine(FreezeTimerCoroutine(_duration));
    }

    protected virtual IEnumerator FreezeTimerCoroutine(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }


    #region �����������

    public void OpenCounterImage()
    {
        canbeStunned = true;
        counterImage.SetActive(true);
    }

    public void CloseCounterImage()
    {
        canbeStunned = false;
        counterImage.SetActive(false);
    }
    public void AnimatorTrigger()
    {
        stateMachine.currentState.triggerCalled = true;
    }

    #endregion

    public virtual bool IsBeStunned()
    {
        if (canbeStunned)
        {
            //OpenCounterImage();
            CloseCounterImage();
            return true;
        }
        else
            return false;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(attackCheck.position, new Vector3(attackCheck.position.x + attackDistance * faceDir, attackCheck.position.y));
    }

 
}
