using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class InventorySlot
    {
        public int ID = -1;
        public Item Item;
        public int Quantity;

        public InventorySlot()
        {
            ID = -1;
            Item = null;
            Quantity = 0;
        }

        public InventorySlot(int pID, Item pItem, int pQuantity)
        {
            Debug.Log($"<size=22><color=white>InventorySlot 02</color></size>");

            ID = pID;
            Item = pItem;
            Quantity = pQuantity;
        }

        public void UpdateSlot(int pID, Item pItem, int pQuantity)
        {
            ID = pID;
            Item = pItem;
            Quantity = pQuantity;
        }

        public void AddAmount(int pValue)
        {
            Quantity += pValue;
        }
    }

    [System.Serializable]
    public class Inventory { public List<InventorySlot> ListItems = new List<InventorySlot>(); }
}