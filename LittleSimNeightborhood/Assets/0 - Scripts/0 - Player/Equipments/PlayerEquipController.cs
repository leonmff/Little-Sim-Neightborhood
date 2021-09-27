using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipController : MonoBehaviour
{
    [SerializeField]
    PlayerEquipment _equipGlasses = null;
    [SerializeField]
    PlayerEquipment _equipBelts = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipGlass(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipGlass(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipGlass(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipGlass(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            EquipBelt(0);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            EquipBelt(1);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            EquipBelt(2);
    }

    private void EquipBelt(int pIndex)
    {
        SOClothOld[] t_equipmentsBelts = Resources.LoadAll<SOClothOld>("Clothes/Belts");
        //_equipBelts.Equip(t_equipmentsBelts[pIndex].Type, t_equipmentsBelts[pIndex].Animator);
    }

    void EquipGlass(int pIndex)
    {
        SOClothOld[] t_equipmentsGlasses = Resources.LoadAll<SOClothOld>("Clothes/Glasses");
        //_equipGlasses.Equip(t_equipmentsGlasses[pIndex].Type, t_equipmentsGlasses[pIndex].Animator);
    }
}
