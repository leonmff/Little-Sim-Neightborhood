using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemType[] AllowedItems = new ItemType[0];
        
        [System.NonSerialized]
        public InventoryController Parent;


        public Item Item;
        public int Quantity;

        public SOItem SOItem
        {
            get
            {
                if (Item.Id >= 0)
                    return Parent.Database.ListItems[Item.Id];

                return null;
            }
        }

        public InventorySlot()
        {
            Item = new Item();
            Quantity = 0;
        }

        public InventorySlot(Item pItem, int pQuantity)
        {
            Item = pItem;
            Quantity = pQuantity;
        }

        public void UpdateSlot(Item pItem, int pQuantity)
        {
            Item = pItem;
            Quantity = pQuantity;
            
            if (Parent)
                Parent.UpdateInventoryInformation(); 
        }

        public void RemoveItem()
        {
            Item = new Item();
            Quantity = 0;
        }

        public void AddAmount(int pValue)
        {
            Quantity += pValue;
        }

        public bool CanPlaceInSlot(SOItem pSOItem)
        {
            if (AllowedItems.Length <= 0 || pSOItem == null || pSOItem.Data.Id < 0)
                return true;

            for (int index = 0; index < AllowedItems.Length; index++)
            {
                if (pSOItem.Type == AllowedItems[index])
                    return true;
            }

            return false;
        }
    }

    [System.Serializable]
    public class Inventory 
    { 
        public List<InventorySlot> ListItems = new List<InventorySlot>();

        public void Clear()
        {
            for (int index = 0; index < ListItems.Count; index++)
            {
                ListItems[index].UpdateSlot(new Item(), 0);
            }
        }
    }
}