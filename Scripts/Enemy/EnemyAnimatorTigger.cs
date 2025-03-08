using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorTigger : MonoBehaviour
{
    private Enemy_Skeleton skeleton => this.GetComponentInParent<Enemy_Skeleton>();
   
    private void AnimatorTrigger()
    {
        skeleton.AnimatorTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(skeleton.attackCheckPos.position, skeleton.attackRadius);
        foreach(Collider2D hitInfo in collider2Ds)
        {
            if(hitInfo.GetComponent<Player>() != null)
            {
                PlayerStats targetStats = hitInfo.GetComponent<PlayerStats>();

                skeleton.stats.DoDamager(targetStats);

                //hitInfo.GetComponent<Player>().DamagEffect();
            }
        }
    }

    private void OpenCounterImage() => skeleton.OpenCounterImage();

    private void CloseCounterImage() => skeleton.CloseCounterImage();
}
