using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;

    private bool isReturning;


    private float returnSpeed;
    private float freeTimeDuration;

    [Header("PericeInfo")]
    private int periceAmount;

    [Header("Spin Info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped; //是否到达最远距离
    private bool isSpinning;

    private float hitTimer;
    private float hitCoolDown;
    private float spinDirection;
    
    [Header("Bounce Info")]
    private  float bounceSpeed;
    private bool isBouncing;
    private int bounceAmount;
    private List<Transform> enemyTargetList = new List<Transform>();
    private int targetIndex;

    private void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        cd = this.GetComponent<CircleCollider2D>();
    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    public void SetupBounce(bool _isBouncing,int _amountOfBounce,float _bounceSpeed)
    {
        bounceAmount = _amountOfBounce;
        isBouncing = _isBouncing;
        bounceSpeed = _bounceSpeed;
    }

    public void SetupPerice(int _periceAmount)
    {
        periceAmount = _periceAmount;
    }

    public void SetupSpin(bool _isSpinning,float _maxTravelDistance,float _spinDuration,float _hitCoolDown)
    {
        isSpinning = _isSpinning;
        maxTravelDistance = _maxTravelDistance;
        spinDuration = _spinDuration;
        hitCoolDown = _hitCoolDown;
    }
    public void SetupSword(Vector2 _launchDir, float _swordGravity, Player _player,float _freeTimeDuration,float _returnSpeed)
    {
        player = _player;
        rb.velocity = _launchDir;
        rb.gravityScale = _swordGravity;

        returnSpeed = _returnSpeed;
        freeTimeDuration = _freeTimeDuration;

        if(periceAmount <=0) //穿透不需要旋转
            animator.SetBool("Rotation", true);

        spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);

        Invoke("DestroyMe", 7);
    }

    public void ReturnSword()
    {
        //rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturning = true;
    }
    private void Update()
    {
        if (isReturning)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(this.transform.position, player.transform.position) < 1)
            {
                player.CatchSword();
            }
        }
        BounceLogic();
        SpinLogic();

    }

    private void SpinLogic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(player.transform.position, this.transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();
            }
            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;

                this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x + spinDirection,
                                                                                                    this.transform.position.y), 1.5f * Time.deltaTime);

                if (spinTimer < 0)
                {
                    isSpinning = false;
                    isReturning = true;
                }
            }
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
                hitTimer = hitCoolDown;
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 1);

                foreach (Collider2D hitInfo in collider2Ds)
                {
                    if (hitInfo.GetComponent<Enemy>() != null)
                    {
                        SwordSkillDamage(hitInfo.GetComponent<Enemy>());    
                    }
                }

            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;//冻结碰撞后的位置变换
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTargetList.Count > 0)
        {

            this.transform.position = Vector2.MoveTowards(this.transform.position, enemyTargetList[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, enemyTargetList[targetIndex].position) < 0.1f)
            {
                SwordSkillDamage(enemyTargetList[targetIndex].GetComponent<Enemy>());
                targetIndex++;
                bounceAmount--;

                if (bounceAmount <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                if (targetIndex >= enemyTargetList.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        if(collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            SwordSkillDamage(enemy);
        }

        SetupTargetsForBounce();

        StuckInfo(collision);

        

    }

    private void SwordSkillDamage(Enemy enemy)
    {
        //enemy.DamagEffect();

        PlayerManager.Instance.player.stats.DoDamager(enemy.GetComponent<CharacterStats>());
        //enemy.StartCoroutine("FreezeTimerFor", freeTimeDuration);

        enemy.FreezeTimerFor(freeTimeDuration);
    }

    private void SetupTargetsForBounce()
    {
        if (isBouncing && enemyTargetList.Count <= 0)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 10);

            foreach (Collider2D hitInfo in collider2Ds)
            {
                if (hitInfo.GetComponent<Enemy>() != null)
                {
                    enemyTargetList.Add(hitInfo.transform);
                }
            }

        }
    }

    private void StuckInfo(Collider2D collision)
    {

        if(periceAmount>0 && collision.GetComponent<Enemy>() != null)
        {
            periceAmount--;
            return;
        }

        if (isSpinning)
        {
            StopWhenSpinning();
            return;
        }

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTargetList.Count > 0)
            return;
        animator.SetBool("Rotation", false);
        this.transform.parent = collision.transform;
    }
}
