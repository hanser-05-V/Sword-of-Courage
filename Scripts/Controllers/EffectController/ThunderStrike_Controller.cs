using UnityEngine;


public class ThunderStrike_Controller : MonoBehaviour
{

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        //碰撞过后造成伤害
        if (collision.GetComponent<Enemy>() != null)
        {
            PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            EnemyStats targetEnemy = collision.GetComponent<EnemyStats>();

            Debug.Log("造成魔法伤害");
            playerStats.DoMagicalDamage(targetEnemy);
        }
    }
    
}
