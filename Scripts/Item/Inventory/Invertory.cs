// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Invertory : MonoBehaviour //�ֿ���
// {
//     private static Invertory instance = new Invertory();
//     public  static Invertory Instance => instance;


//     public List<ItemData> startingEquipment = new List<ItemData>();
         

//     public List<InventoryItem> equipment = new List<InventoryItem>(); //�����б�
//     public Dictionary<ItemData_Equipment,InventoryItem> equipmentDic = new Dictionary<ItemData_Equipment,InventoryItem>();  
   

//     public List<InventoryItem> inventoryItems = new List<InventoryItem>(); //��Ʒ������
//     public Dictionary<ItemData,InventoryItem> inventoryDic = new Dictionary<ItemData,InventoryItem>();  //���������࣬��Ʒ������

//     public List<InventoryItem> stashItems = new List<InventoryItem>();
//     public Dictionary<ItemData,InventoryItem> stashDic = new Dictionary<ItemData,InventoryItem>();  


//     [Header("Invertory UI")]
//     [SerializeField] private Transform invertorySlotParent;
//     [SerializeField] private Transform stashSlotParent;
//     [SerializeField] private Transform equpmentSlotParent;
//     [SerializeField] private Transform statSlotParent;
    

//     // private UI_ItemSlot[] inventoryItemSlot;
//     // // private UI_ItemSlot[] stashSlot;
//     // private UI_EquipmentSlot[] equipmentSlot;
//     // private UI_StatSlot[] statSlot;

//     [Header("Item coolDown")]
//     private float lastTimeUsedFlask;
//     private float lastTimeUsedArmor;

//     private float flaskCooldown;
//     private float armorCooldown;

//     private void Awake()
//     {
//         instance = this;
//     }

//     private void Start()
//     {
//         inventoryItemSlot = invertorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
//         stashSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
//         equipmentSlot = equpmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

//         statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

//         AddStartingItems();
//     }

    


//     private void AddStartingItems()
//     {
//         for(int i= 0; i<startingEquipment.Count; i++)
//         {
//             if (startingEquipment[i] != null)
//                 AddItem(startingEquipment[i]);
//         }
//     }

//     public void EquipItem(ItemData _item)
//     {
//         ItemData_Equipment newEquipment = _item as ItemData_Equipment;
//         InventoryItem newItem = new InventoryItem(newEquipment);

//         ItemData_Equipment oldEquipment = null;

//         foreach(KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDic)
//         {
//             if(item.Key.equipmentType == newEquipment.equipmentType)
//             {
//                 oldEquipment = item.Key;
//             }
//         }

//         if(oldEquipment != null)
//         {
//             Unequipment(oldEquipment);
//             AddItem(oldEquipment);
//             //AddItem(oldEquipment);
//         }


//         equipment.Add(newItem);
//         equipmentDic.Add(newEquipment, newItem);

//         newEquipment.AddModifiers();

//         RemoveItem(_item);

        
//         UpdaeSlotUI();
//     }

//     public void Unequipment(ItemData_Equipment itemToRemove)
//     {
//         if (equipmentDic.TryGetValue(itemToRemove, out InventoryItem value))
//         {
            
//             equipment.Remove(value);
//             equipmentDic.Remove(itemToRemove);
//             itemToRemove.RemoveModifiers(); 
//         }
//     }


//     public void UpdaeSlotUI()
//     {


//         for(int i = 0; i < equipmentSlot.Length; i++)
//         {
//             foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDic)
//             {
//                 if (item.Key.equipmentType == equipmentSlot[i].slotType)
//                 {
//                     equipmentSlot[i].UpdateSlot(item.Value);
                    
//                 }
//             }

//         }


//         for (int i = 0; i < inventoryItemSlot.Length; i++)
//         {
//             inventoryItemSlot[i].CleanUpSlot();
//         }
//         for(int i = 0; i<stashSlot.Length; i++)
//         {
//             stashSlot[i].CleanUpSlot();     
//         }

//         for(int i = 0; i < inventoryItems.Count; i++)
//         {
//             inventoryItemSlot[i].UpdateSlot(inventoryItems[i]);
//         }

//         for(int i = 0;i < stashItems.Count; i++)
//         {
//             stashSlot[i].UpdateSlot(stashItems[i]);
//         }

//         for(int i=0;i<statSlot.Length; i++)
//         {
//             statSlot[i].UpdateStatValueUI();
//         }
//     }

//     public void AddItem(ItemData _item)
//     {
//         switch (_item.itemType)
//         {
//             case E_ItemType.Material:

//                 AddToInventory(_item);
//                 break;
//             case E_ItemType.Equipment:

//                 if (CanAddItem())
//                     AddToStash(_item);
//                 break;
//         }
  
//         UpdaeSlotUI();

//     }

//     public bool CanAddItem()
//     {
//         if(inventoryItems.Count >= inventoryItemSlot.Length)
//         {
//             Debug.Log("װ���������");
//             return false;
//         }
//         return true;
//     }

//     private void AddToStash(ItemData _item)
//     {
       
//         if (stashDic.TryGetValue(_item, out InventoryItem value))
//         {
//             value.AddStack();
//         }
//         else
//         {
//             InventoryItem newItem = new InventoryItem(_item);

//             stashItems.Add(newItem);
//             stashDic.Add(_item, newItem);
//         }
//     }


//     private void AddToInventory(ItemData _item)
//     {
//         //TryGetValue �᳢�Ի�ȡ�ֵ��е�ֵ�������ز���ֵ��ʾ�Ƿ�ɹ��ҵ��ü���������������Ҫͬʱ��ȡֵ���������Ե������
//         if (inventoryDic.TryGetValue(_item, out InventoryItem value))
//         {
//             value.AddStack();
//         }
//         else
//         {
//             InventoryItem newItem = new InventoryItem(_item);

//             inventoryItems.Add(newItem);
//             inventoryDic.Add(_item, newItem);
//         }
//     }

//     /// <summary>
//     /// �Ƴ���Ʒ
//     /// </summary>
//     /// <param name="_item"></param>
//     public void RemoveItem(ItemData _item)
//     {
//         if (inventoryDic.ContainsKey(_item))
//         {
//             if (inventoryDic[_item].stackSize <= 1)
//             {
//                 inventoryItems.Remove(inventoryDic[_item]);
//                 inventoryDic.Remove(_item);
//             }
//             else
//                 inventoryDic[_item].RemoveStack();
//         }

//         if(stashDic.TryGetValue(_item,out InventoryItem value))
//         {
//             if(value.stackSize <= 1)
//             {
//                 stashItems.Remove(value);
//                 stashDic.Remove(_item);
//             }
//             else
//                 value.RemoveStack();
//         }
//         UpdaeSlotUI ();
//     }

//     /// <summary>
//     /// �ܷ�ϳ�����
//     /// </summary>
//     /// <param name="_itemToCraft"></param>
//     /// <param name="_requireMaterials"></param>
//     /// <returns></returns>
//     public bool CanCraft(ItemData_Equipment _itemToCraft,List<InventoryItem> _requireMaterials)
//     {
//         List<InventoryItem> materialsToRemove = new List<InventoryItem>();

//         for(int i = 0; i < _requireMaterials.Count; i++)
//         {
//             if (inventoryDic.TryGetValue(_requireMaterials[i].data , out InventoryItem inventoryValue))
//             {
//                 if(inventoryValue.stackSize < _requireMaterials[i].stackSize)
//                 {
//                     Debug.Log("������������");
//                     return false;
//                 }
//                 else
//                 {
//                     materialsToRemove.Add(inventoryValue);
//                 }
//             }
//             else
//             {
//                 Debug.Log("���ϲ���");
//                 return false;
//             }
//         }

//         for(int i=0;i< materialsToRemove.Count; i++)
//         {
//             RemoveItem(materialsToRemove[i].data);
//         }

//         AddItem(_itemToCraft);

//         Debug.Log("�ɹ���������" +  _itemToCraft.name);

//         return true;

//     }

//     /// <summary>
//     /// �õ���ǰ�����ֿ� �����������
//     /// </summary>
//     /// <returns></returns>
//     public List<InventoryItem> GetEqiupmentList()
//     {
//         return equipment;
//     }

//     /// <summary>
//     /// �õ���ǰ���ϲֿ����� �����������
//     /// </summary>
//     /// <returns></returns>
//     public List <InventoryItem> GetStashList()
//     {
//         return stashItems;
//     }

//     /// <summary>
//     /// �õ���ǰװ�������� --����Ч���ж� 
//     /// </summary>
//     /// <param name="_type"></param>
//     /// <returns></returns>
//     public ItemData_Equipment GetEquipment(E_EquipmentType _type)
//     {
//         ItemData_Equipment eqiupment = null;
        
//         foreach(KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDic)
//         {
//             if(item.Key.equipmentType == _type)
//             {
//                 eqiupment = item.Key;
//             }
//         }
//         return eqiupment;
//     }

//     /// <summary>
//     /// ʹ����ƷЧ��
//     /// </summary>
//     public void UseFlask()
//     {
//         ItemData_Equipment currentFlask = GetEquipment(E_EquipmentType.Flask); //�õ���ǰװ��

//         if (currentFlask == null)
//         {
//             return;
//         }

//         bool canUseFlask = Time.time > lastTimeUsedFlask + flaskCooldown;
//         if (canUseFlask)
//         {
//             flaskCooldown = currentFlask.itemCoolDown;
//             currentFlask.Effect(null);
//             Debug.Log("ʹ��ҩƷ"  + currentFlask);
//             lastTimeUsedFlask = Time.time;
//         }
//         else
//         {
//             Debug.Log("����������ȴ��");
//         }

//     }

//     /// <summary>
//     /// �ܷ�ʹ�ÿ�������Ч��
//     /// </summary>
//     /// <returns></returns>
//     public bool CanUseArmor()
//     {
//         ItemData_Equipment currentArmor = GetEquipment(E_EquipmentType.Armor);

//         if(currentArmor == null)
//             return false;

//         if(Time.time > lastTimeUsedArmor + armorCooldown)
//         {
//             armorCooldown = currentArmor.itemCoolDown;
//             lastTimeUsedArmor = Time.time;
//             return true;
//         }
//         else
//         {
//             Debug.Log("��������Ч��������ȴ��");
//             return false;
//         }
//     }
//     private void Update()
//     {

//         //if (Input.GetKeyDown(KeyCode.L))
//         //{
//         //    ItemData newItem = inventoryItems[inventoryItems.Count - 1].data;
//         //    Invertory.Instance.RemoveItem(newItem);
//         //}
//     }
// }
