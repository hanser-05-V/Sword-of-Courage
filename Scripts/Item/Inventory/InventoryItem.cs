using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class InventoryItem //仓库物品类
{
    public ItemData data; //仓库物品
    public int stackSize; //物品数量

    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        AddStack();//让初始值不为0 
    }

    //增开物品数量
    public void AddStack()
    {
        stackSize++;
    }
    //减少物品数量
    public void RemoveStack()
    {
        stackSize--;
    }
}
