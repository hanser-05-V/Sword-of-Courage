using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Start()
    {
        base.Start();
       
        player = PlayerManager.Instance.player;
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        player.DamagEffect();

    }
    protected override void Die()
    {
        base.Die();
        player.Die();

       
        GetComponent<PlayerItemDrop>().GenerateDrop();
    }

    protected override void DecreaseHpBy(int _damage)
    {
        base.DecreaseHpBy(_damage);

        // ItemData_Equipment currentArmor = Invertory.Instance.GetEquipment(E_EquipmentType.Armor);

        // if (currentArmor != null)
        // {
        //     Debug.Log("��ǰ���λ��" + player.transform.position);
        //     currentArmor.Effect(player.transform);  
        // }
    }
}
