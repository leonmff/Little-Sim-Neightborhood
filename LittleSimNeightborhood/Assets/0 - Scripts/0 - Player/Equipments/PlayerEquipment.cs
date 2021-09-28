using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : PlayerAnimation
{
    public enum EquipType { Glasses, Belt }

    [SerializeField]
    EquipType _type;
    public EquipType Type { get => _type; }

    public void Equip(RuntimeAnimatorController pAnimator)
    {
        //Type = pType;
        _anim.runtimeAnimatorController = pAnimator;
        
        SetAnimationParameters(_playerMovement.DirectionLast);
    }

    public void Clear()
    {
        _anim.runtimeAnimatorController = null;
        _sr.sprite = null;
    }
}
