using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
    public class EquipmentObject : SOItem
    {
        private void Awake()
        {
            Type = ItemType.Axe;
        }
    }
}
