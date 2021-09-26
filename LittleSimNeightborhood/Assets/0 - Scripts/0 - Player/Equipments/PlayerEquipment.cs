using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : PlayerAnimation
{
    public enum EquipType { Glasses, Belt }

    [SerializeField, InspectorReadOnly]
    protected EquipType Type;

    public void Equip(EquipType pType, RuntimeAnimatorController pAnimator)
    {
        Type = pType;
        _anim.runtimeAnimatorController = pAnimator;
    }
}
