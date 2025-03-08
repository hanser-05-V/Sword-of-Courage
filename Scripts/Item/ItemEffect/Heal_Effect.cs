using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]
public class Heal_Effect : ItemEffect
{
    [Range(0f,1f)]
    [SerializeField] private float healPercent;
    public override void ApplyEffect(Transform _targetPos)
    {
        PlayerStats  playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

        int healAmount = Mathf.RoundToInt( playerStats.GetMaxHealthValue() * healPercent);

        playerStats.IncreaseHpBy(healAmount);
    }
}
