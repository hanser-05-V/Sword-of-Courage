using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{
    [SerializeField] private int baseValue;

    public List<int> modifiers = new List<int>(); //buff����
    

    public int GetValue()
    {
        int finalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            finalValue+=modifier;
        }

        return finalValue;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }
    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);
    }
    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }


}
