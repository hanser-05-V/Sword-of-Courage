using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum E_EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask,
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public E_EquipmentType equipmentType;
    public ItemEffect[] itemEffects;

    public float itemCoolDown;

    [Header("Major Stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive Stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive Stats")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic Stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;


    private int descriptionLength;
    public void Effect(Transform _targetPos)
    {
        foreach (ItemEffect item in itemEffects)
        {
            item.ApplyEffect(_targetPos);
        }
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();    

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);


        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.maxHealth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);


        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;
        AddItemDescription(strength, "����");
        AddItemDescription(agility, "����");
        AddItemDescription(intelligence, "����");
        AddItemDescription(vitality, "����");

        AddItemDescription(damage, "�����˺�");
        AddItemDescription(critChance, "������");
        AddItemDescription(critPower, "�����˺�");

        AddItemDescription(health, "Ѫ��");
        AddItemDescription(evasion, "������");
        AddItemDescription(armor, "�￹");
        AddItemDescription(magicResistance, "ħ��");

        AddItemDescription(fireDamage, "�����˺�");
        AddItemDescription(iceDamage, "�����˺�");
        AddItemDescription(lightingDamage, "�׵��˺�");

        if(descriptionLength < 5)
        {
            for (int i = 0; i < 5-descriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            if(_value >0)
            {
                sb.Append(_name + ":" + "" + _value);
            }
            descriptionLength++;
        }
    }
}
