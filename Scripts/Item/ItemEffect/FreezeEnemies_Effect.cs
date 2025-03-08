using UnityEngine;


[CreateAssetMenu(fileName = "Freeze Enemies Effect", menuName = "Data/Item Effect/Freeze Enemies")]
public class FreezeEnemies_Effect : ItemEffect
{

    [SerializeField] private float freezeTimeDuration;

    private PlayerStats playerStats;
    public override void ApplyEffect(Transform _targetPos)
    {

        playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

        if (playerStats.currentHp >= playerStats.GetMaxHealthValue() * 0.1f)
        {
            return;
        }
        // if (!Invertory.Instance.CanUseArmor())
        // {
        //     return;
        // }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_targetPos.position, 2);//��ȡ��Χ�� �뾶2�� ��ײ��

        foreach(Collider2D collider in colliders)
        {
            collider.GetComponent<Enemy>()?.FreezeTimerFor(freezeTimeDuration);
        }

    }
}
