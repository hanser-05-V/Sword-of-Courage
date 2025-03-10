using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{

    [Header("击退相关 ")]
    [SerializeField] protected Vector2 knockBackDirection;
    [SerializeField] protected float knockTime;
    protected bool isKnocked;

    [Header("碰撞检测距离相关")]
    public Transform attackCheckPos;
    public float attackRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] private float groundCheckDis;
    [SerializeField] protected Transform walllCheck;
    [SerializeField] private float walllChaeckDis;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Dead Info")]
    [SerializeField] private float dispareTime;

    //面朝方向 确保只能得不能改
    public int faceDir { get; private set; } = 1;
    protected bool isFaceRight = true;

    #region 组件相关
    public Animator animator { get; private set; }
    public EnitityFX fx { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }

    public CharacterStats stats { get; private set; }


    public CapsuleCollider2D cr { get; private set; }
    #endregion

    public UnityAction onFlipped;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

        animator = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        fx = this.GetComponent<EnitityFX>();

        sr = this.GetComponentInChildren<SpriteRenderer>();

        stats = this.GetComponent<CharacterStats>();

        cr = this.GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update()
    {

    }

    protected virtual IEnumerator HitKnocback(float knockTimer)
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockBackDirection.x * -faceDir, knockBackDirection.y);
        yield return new WaitForSeconds(knockTimer);
        isKnocked = false;
    }

    // 伤害特效
    public void DamagEffect()
    {
       // fx.StartCoroutine("FlashFx");
        StartCoroutine("HitKnocback", knockTime);
    }

    #region 速度设置相关
    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(0, 0);
    }
    //设置速度
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
        {

            return;
        }
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FilpController(xVelocity);
    }

    #endregion

    #region 角色翻转方向相关
    //翻转方法
    public virtual void Filp()
    {
        faceDir = faceDir * -1;
        isFaceRight = !isFaceRight;
        this.transform.Rotate(0, 180, 0);

        onFlipped?.Invoke();
    }

    //翻转控制器
    public virtual void FilpController(float xVelocity)
    {
        if (xVelocity > 0 && !isFaceRight)
            Filp();
        else if (xVelocity < 0 && isFaceRight)
            Filp();
    }

    #endregion

    #region 碰撞检测相关
    //是否检测到地面
    public bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatIsGround);
    }

    //是否检测到墙壁
    public bool IsWallDetected()
    {
        return Physics2D.Raycast(walllCheck.position, Vector2.right * faceDir, walllChaeckDis, whatIsGround);
    }
    //划线检测距离
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDis));
        Gizmos.DrawLine(walllCheck.position, new Vector3(walllCheck.position.x + walllChaeckDis, walllCheck.position.y));

        Gizmos.DrawWireSphere(attackCheckPos.position, attackRadius);
    }

    #endregion


    public virtual void SlowEnityBy(float _slowPercentage,float _slowDuration)
    {

    } 
    protected virtual void ReturnDefultSpeed()
    {
        animator.speed = 1;
    }

    public virtual void Die()
    {
        Invoke("DestorySelf", dispareTime);
    }

    //self
    public virtual void DestorySelf()
    {
        Destroy(this.gameObject);
    }
}
