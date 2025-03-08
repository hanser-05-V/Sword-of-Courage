using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//public enum E_BuffType 
//{
//    strength,
//    agility,
//    intelegence,
//    vitality,

//    damage,
//    critChance,
//    critPower,

//    maxhealth,
//    armor,
//    evasion,
//    magicResistance,

//    fireDamage,
//    iceDamage,
//    lightingDamage,

//}
[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]

public class Buff_Effect : ItemEffect
{
    private PlayerStats stats;

    [SerializeField] private E_StatType buffType;
    [SerializeField] private int buffAmount;
    [SerializeField] private float buffDuration;

    public override void ApplyEffect(Transform _targetPos)
    {
        stats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

        stats.IncreaseStatsBy(buffAmount, buffDuration, stats.GetStats(buffType));
    }


    //public Stats GetStats()
    //{

    //    switch (buffType)
    //    {
    //        case E_BuffType.strength: return stats.strength;       
    //        case E_BuffType.agility: return stats.agility;    
    //        case E_BuffType.intelegence: return stats.intelligence;
    //        case E_BuffType.vitality: return stats.vitality;   
    //        case E_BuffType.damage: return stats.damage;
    //        case E_BuffType.critChance: return stats.critChance;
    //        case E_BuffType.critPower: return stats.critPower;
    //        case E_BuffType.maxhealth: return stats.maxHealth;
    //        case E_BuffType.armor: return stats.armor;
    //        case E_BuffType.evasion: return stats.evasion;
    //        case E_BuffType.magicResistance: return stats.magicResistance;
    //        case E_BuffType.fireDamage: return stats.fireDamage;
    //        case E_BuffType.iceDamage: return stats.iceDamage;
    //        case E_BuffType.lightingDamage: return stats.lightingDamage;
    //    }
    //    return null;
    //}
}

