using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int amountOfItems;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();


    [SerializeField] private GameObject dropPrefab;



    public virtual void GenerateDrop()
    {

        if (possibleDrop.Length <= 0) //没有可掉落物，不执行
            return;

        for(int i = 0;i<possibleDrop.Length;i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
            {
                dropList.Add(possibleDrop[i]);
            }
        }

        for(int i = 0; i < amountOfItems; i++)
        {
            if (dropList.Count<=0) //每次掉落都会随机移除 如果列表为空，则不在掉落
                return;

            ItemData randomItem = dropList[Random.Range(0,dropList.Count-2)];

            dropList.Remove(randomItem);
            DropItem(randomItem);
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, this.transform.position,Quaternion.identity) ;

        Vector2 randomVelocity = new Vector2(Random.Range(-5,5),Random.Range(15,17));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
