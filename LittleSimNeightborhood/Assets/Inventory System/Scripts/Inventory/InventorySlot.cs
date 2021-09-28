using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public delegate void SlotUpdated(InventorySlot pInventorySlot);

    [System.Serializable]
    public class InventorySlot
    {
        public ItemType[] AllowedItems = new ItemType[0];
        
        [System.NonSerialized]
        public InventoryController Parent;

        [System.NonSerialized]
        public GameObject SlotDisplay;

        [System.NonSerialized]
        public SlotUpdated OnBeforeUpdate;
        [System.NonSerialized]
        public SlotUpdated OnAfterUpdate;

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

        public InventorySlot() => UpdateSlot(new Item(), 0);

        public InventorySlot(Item pItem, int pQuantity) => UpdateSlot(pItem, pQuantity);

        public void UpdateSlot(Item pItem, int pQuantity)
        {
            OnBeforeUpdate?.Invoke(this);

            Item = pItem;
            Quantity = pQuantity;

            OnAfterUpdate?.Invoke(this);
        }

        public void RemoveItem() => UpdateSlot(new Item(), 0);

        public void AddAmount(int pValue) => UpdateSlot(Item, Quantity += pValue);

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
        public List<InventorySlot> ListInventorySlots = new List<InventorySlot>();

        public void Clear()
        {
            for (int index = 0; index < ListInventorySlots.Count; index++)
            {
                ListInventorySlots[index].UpdateSlot(new Item(), 0);
            }
        }
    }
}