using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelection : MonoBehaviour
{
    [SerializeField, Header("Currently Equipped Buttons")]
    InventorySlotInformations _equippedSlotBelt = null;
    [SerializeField]
    InventorySlotInformations _equippedSlotGlasses = null;

    public void Select(InventorySelection pInventorySelection)
    {
        
    }

    // Qual o tipo (Cloth, Food, Equipment)
    // Especifidades
    // - Se for Cloth, qual tipo? Glasses, Belt, etc...
    // - Se for food, qual tipo? Health, Stamina, etc...
    // - Se for equipment, qual tipo? Axe, Shovel, etc...
}
