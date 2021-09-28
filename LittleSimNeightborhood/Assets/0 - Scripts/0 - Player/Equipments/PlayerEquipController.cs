using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class PlayerEquipController : MonoBehaviour
{
    [SerializeField]
    PlayerEquipment _equipGlasses = null;
    [SerializeField]
    PlayerEquipment _equipBelts = null;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //        EquipGlass(0);
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //        EquipGlass(1);
    //    else if (Input.GetKeyDown(KeyCode.Alpha3))
    //        EquipGlass(2);
    //    else if (Input.GetKeyDown(KeyCode.Alpha4))
    //        EquipGlass(3);
    //    else if (Input.GetKeyDown(KeyCode.Alpha5))
    //        EquipBelt(0);
    //    else if (Input.GetKeyDown(KeyCode.Alpha6))
    //        EquipBelt(1);
    //    else if (Input.GetKeyDown(KeyCode.Alpha7))
    //        EquipBelt(2);
    //}

    public void EquipCloth(SOItem pSOItem)
    {
        //SOClothOld[] t_equipmentsBelts = Resources.LoadAll<SOClothOld>("Clothes/Belts");
        //_equipBelts.Equip(t_equipmentsBelts[pIndex].Item.Type, t_equipmentsBelts[pIndex].Animator);

        SOClothObject t_cloth = (SOClothObject)pSOItem;

        switch (t_cloth.Type)
        {
            case ItemType.Glasses:
                _equipGlasses.Equip(t_cloth.Animator);
                break;
            case ItemType.Belt:
                _equipBelts.Equip(t_cloth.Animator);
                break;
            case ItemType.Food:
                break;
            case ItemType.Axe:
                break;
            case ItemType.Default:
                break;
            default:
                break;
        }
    }

    public void RemoveCloth(SOItem pSOItem)
    {
        SOClothObject t_cloth = (SOClothObject)pSOItem;

        switch (t_cloth.Type)
        {
            case ItemType.Glasses:
                _equipGlasses.Clear();
                break;
            case ItemType.Belt:
                _equipBelts.Clear();
                break;
            case ItemType.Food:
                break;
            case ItemType.Axe:
                break;
            case ItemType.Default:
                break;
            default:
                break;
        }
    }

    //void EquipGlass(int pIndex)
    //{
    //    //SOClothOld[] t_equipmentsGlasses = Resources.LoadAll<SOClothOld>("Clothes/Glasses");
    //    //_equipGlasses.Equip(t_equipmentsGlasses[pIndex].Item.Type, t_equipmentsGlasses[pIndex].Animator);
    //}
}
