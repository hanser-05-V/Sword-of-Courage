using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's DropChance")]
    [SerializeField] private float chanceTolooseEquipment;
    [SerializeField] private float chanceTolooseMaterials;

    public override void GenerateDrop()
    {
        // Invertory inventory = Invertory.Instance;

        List<InventoryItem> itemsToUnquip = new List<InventoryItem>();
        List<InventoryItem> materialsToLoose = new List<InventoryItem>();


        // foreach (InventoryItem item in inventory.GetEqiupmentList())
        // {
        //     if(Random.Range(0,101) <= chanceTolooseEquipment)
        //     {
        //         DropItem(item.data);
        //         itemsToUnquip.Add(item);
        //     }
        // }

        // for(int i = 0; i < itemsToUnquip.Count; i++)
        // {
        //     inventory.Unequipment(itemsToUnquip[i].data as ItemData_Equipment);
        // }

        // foreach(InventoryItem item in inventory.GetStashList())
        // {
        //     if(Random.Range(0,101) <= chanceTolooseMaterials)
        //     {
        //         DropItem(item.data);
        //         materialsToLoose.Add(item);
        //     }
        // }

        // for(int i= 0; i < materialsToLoose.Count; i++)
        // {
        //     inventory.RemoveItem(materialsToLoose[i].data);
        // }

    }

}
