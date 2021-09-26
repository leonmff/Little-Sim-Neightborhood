using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Items/Equipment")]
public class SOEquipment : ScriptableObject
{
    public string Name;
    public string Price;

    [Space(7)]
    public PlayerEquipment.EquipType Type;
    public RuntimeAnimatorController Animator;
}
