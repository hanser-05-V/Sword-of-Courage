using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    [SerializeField] private ItemData itemData;

    [SerializeField] private Rigidbody2D rb;
   

    public void SetupVisuals()
    {
        if (itemData == null)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object --" + itemData.name;
    }

    public void PickupItem()
    {

        // if(!Invertory.Instance.CanAddItem() && itemData.itemType == E_ItemType.Equipment)
        // {
            
        //     rb.velocity = new Vector2(0, 7);
        //     return;
        // }    

        // Invertory.Instance.AddItem(itemData);
        // Destroy(this.gameObject);
    }

    public void SetupItem(ItemData _itemData,Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity =_velocity;

        SetupVisuals();
    }
    
   
}
