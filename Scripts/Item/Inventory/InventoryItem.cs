using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class InventoryItem //�ֿ���Ʒ��
{
    public ItemData data; //�ֿ���Ʒ
    public int stackSize; //��Ʒ����

    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        AddStack();//�ó�ʼֵ��Ϊ0 
    }

    //������Ʒ����
    public void AddStack()
    {
        stackSize++;
    }
    //������Ʒ����
    public void RemoveStack()
    {
        stackSize--;
    }
}
