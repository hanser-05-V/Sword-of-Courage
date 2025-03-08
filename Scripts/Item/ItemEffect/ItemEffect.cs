using UnityEngine;

//[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Item Effect")]
public class ItemEffect : ScriptableObject
{
    public virtual void ApplyEffect(Transform _targetPos)
    {
        Debug.Log("应用特殊效果");
    }
}
