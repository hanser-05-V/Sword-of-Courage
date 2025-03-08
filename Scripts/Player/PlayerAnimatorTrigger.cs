using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    private Player player => this.gameObject.GetComponentInParent<Player>();

    private void AnimatorTrigger()
    {
        player.AnimatorTigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(player.attackCheckPos.position, player.attackRadius);
        foreach(Collider2D hitInfo in collider2Ds)
        {
            if(hitInfo.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hitInfo.GetComponent<EnemyStats>();
                if(_target != null)
                     player.stats.DoDamager(_target);

                // Invertory.Instance.GetEquipment(E_EquipmentType.Weapon)?.Effect(_target.transform);

            }
        }
    }

    protected void ThrowSword()
    {
        player.skill.sword.CreatSword();
    }
}
