using System.Text;
using UnityEngine;

public enum E_ItemType
{
    Material,
    Equipment,
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public E_ItemType itemType;
    public string itemName;
    public Sprite icon;

    [Range(0, 100)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }
}

