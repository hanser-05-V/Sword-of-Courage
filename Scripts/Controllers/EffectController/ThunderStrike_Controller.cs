using UnityEngine;


public class ThunderStrike_Controller : MonoBehaviour
{

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        //��ײ��������˺�
        if (collision.GetComponent<Enemy>() != null)
        {
            PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            EnemyStats targetEnemy = collision.GetComponent<EnemyStats>();

            Debug.Log("���ħ���˺�");
            playerStats.DoMagicalDamage(targetEnemy);
        }
    }
    
}
