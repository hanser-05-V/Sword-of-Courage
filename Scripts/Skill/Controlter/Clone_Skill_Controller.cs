using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
  
    private float cloneTimer;
    [SerializeField] private Animator animator;
    [SerializeField] private float colorLoseSpeed;
    private SpriteRenderer sr;

    [SerializeField] private Transform attackCheckPos;
    [SerializeField] private float attackRaduis = 0.8f;
    private Transform closestEnemy;

    private bool canDuplicateClone;
    private float chanceToDuplicate;
    private int facingDir = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (sr.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime) * colorLoseSpeed);
        }
    }
    public void SetupClone(Transform newPosition,float _cloneDuration,bool canAttack, Vector3 _offset,Transform _closestEnemy,bool _canDuplicateClone,float _chanceToDuplicate)
    {
        
        this.transform.position = newPosition.position + _offset;
        cloneTimer = _cloneDuration;

        closestEnemy = _closestEnemy;

        canDuplicateClone = _canDuplicateClone;
        chanceToDuplicate = _chanceToDuplicate;

        if(canAttack)
        {
            animator.SetInteger("AttackNumber", Random.Range(1,4));
        }
        FaceClosesTarget();
    }

    private void AnimatorTrigger()
    {
        cloneTimer = -1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(attackCheckPos.position, attackRaduis);
        foreach (Collider2D hitInfo in collider2Ds)
        {
            if (hitInfo.GetComponent<Enemy>() != null)
            {
                //hitInfo.GetComponent<Enemy>().DamagEffect();
                PlayerManager.Instance.player.stats.DoDamager(hitInfo.GetComponent<CharacterStats>());

                if(canDuplicateClone)
                {
                    if(Random.Range(1,100) < chanceToDuplicate) 
                    {
                        SkillManager.Instance.clone.CreatClone(hitInfo.transform, new Vector3(0.5f * facingDir, 0));   
                    }
                }
            }
        }
    }
    private void FaceClosesTarget()
    {
        #region 找到最近敌人逻辑
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position,25);

        //float closestDistance = Mathf.Infinity;

        //foreach (Collider2D hitInfo in colliders)
        //{
        //    if(hitInfo.GetComponent<Enemy>() !=null)
        //    {
        //        float distanceToEnemy = Vector2.Distance(this.transform.position, hitInfo.transform.position);
        //        if(distanceToEnemy < closestDistance)
        //        {
        //            closestDistance = distanceToEnemy;
        //            closestEnemy = hitInfo.transform;
        //        }
        //    }
        //}
        #endregion
        if (closestEnemy != null)
        {
            if(this.transform.position.x> closestEnemy.position.x )
            {
                //成功创建克隆 克隆方向为创建对立方向
                facingDir = -1;
                this.transform.Rotate(0, 180, 0);
            }

        }
    }
}
