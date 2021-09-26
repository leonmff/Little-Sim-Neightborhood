using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipController : MonoBehaviour
{
    [SerializeField]
    PlayerEquipment _equipGlasses = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            EquipGlass();
    }

    void EquipGlass()
    {
        SOEquipment[] t_equipments = Resources.LoadAll<SOEquipment>("Equipments");
        _equipGlasses.Equip(t_equipments[0].Type, t_equipments[0].Animator);
    }
}
