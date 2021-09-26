using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOItem : ScriptableObject
{
    public PlayerEquipment.EquipType Type;

    [Space(7)]
    public string Name;
    public bool Stackable;
    public Sprite Icon;
    public Sprite Background;
    public int Price;
}
