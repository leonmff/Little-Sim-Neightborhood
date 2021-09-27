using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
    public class EquipmentObject : SOItem
    {
        public float atkBonus;
        public float defenceBonus;

        private void Awake()
        {
            Type = ItemType.Equipment;
        }
    }
}
