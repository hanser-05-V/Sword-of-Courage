using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
   [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.Instance.player;
    }
    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            //使用技能
            UseSkill();
            cooldownTimer = cooldown;
            return true;    
        }
        Debug.Log("技能正在冷却");
        return false;
    }
    public abstract void UseSkill();
    protected virtual Transform FindClosetEnemy(Transform _checkTransform) 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        float closestDistance = Mathf.Infinity;

        Transform  closestEnemy = null;

        foreach (Collider2D hitInfo in colliders)
        {
            if (hitInfo.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hitInfo.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hitInfo.transform;
                }
            }
        }

        return closestEnemy;
    }
}
