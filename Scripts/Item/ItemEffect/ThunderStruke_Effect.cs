using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThunderStrike", menuName = "Data/Item Effect/ThunderStrike")]
public class ThunderStruke_Effect : ItemEffect
{

    [SerializeField] private GameObject thunderStrikePrefabs;
    public override void ApplyEffect(Transform _targetPos)
    {
        GameObject newThunderStrike = GameObject.Instantiate(thunderStrikePrefabs, _targetPos.position, Quaternion.identity);

        Destroy(newThunderStrike, 1);
    }
}
