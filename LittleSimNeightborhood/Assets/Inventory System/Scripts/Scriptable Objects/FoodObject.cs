using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
    public class FoodObject : SOItem
    {
        public int restoreHealthValue;

        private void Awake()
        {
            Type = ItemType.Food;
        }
    }
}
