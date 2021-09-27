using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemOld : MonoBehaviour
{
    public PlayerEquipment.EquipType Type;
    public bool Stackable;

    [Space(7)]
    public string Name;
    public int Price;

    [Space(7)]
    public Sprite Icon;
    public Sprite Background;
}
