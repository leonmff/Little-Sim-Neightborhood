using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class SOInventory : ScriptableObject
    {
        [SerializeField]
        string _saveFileName = "";

        [Space(15)]
        public SOItemDatabase database;

        [Space(7)]
        public bool EncryptSaveFiles = true;
        public bool AutoLoadOnAwake = true;
        public bool AutoSaveOnInventory = true;
        public bool ConstantAllowedItems = false;

        [Space(7)]
        public int DefaultInventorySize;

        [Space(7)]
        public Inventory Container;

        public List<InventorySlot> ListInventorySlots { get => Container.ListInventorySlots; }

        string SavePath { get => string.Concat(Application.persistentDataPath, "/", _saveFileName); }

        private void OnEnable()
        {
            if (AutoLoadOnAwake)
                Load();
        }

        public bool AddItem(Item pItem, int pQuantity)
        {
            if (EmptySlotCount <= 0)
                return false;

            if (!pItem.Stackable)
                SetItemToFirstEmptySlot(pItem, pQuantity);
            else
            {
                InventorySlot t_inventorySlot = ListInventorySlots.Find(t_element => t_element.Item.Id == pItem.Id);
                if (t_inventorySlot != null)
                    t_inventorySlot.AddAmount(pQuantity);
                else
                    SetItemToFirstEmptySlot(pItem, pQuantity);
            }

            if (AutoSaveOnInventory)
                Save();

            return true;
        }

        public int EmptySlotCount
        {
            get
            {
                int t_counter = 0;

                for (int index = 0; index < ListInventorySlots.Count; index++)
                {
                    if (ListInventorySlots[index].Item.Id <= -1)
                        t_counter++;
                }

                return t_counter;
            }
        }

        public void SwapItem(InventorySlot pInventorySlot1, InventorySlot pInventorySlot2)
        {
            if (pInventorySlot2.CanPlaceInSlot(pInventorySlot1.SOItem) && pInventorySlot1.CanPlaceInSlot(pInventorySlot2.SOItem))
            {
                InventorySlot t_tempInventorySlot2 = new InventorySlot(pInventorySlot2.Item, pInventorySlot2.Quantity);
                pInventorySlot2.UpdateSlot(pInventorySlot1.Item, pInventorySlot1.Quantity);
                pInventorySlot1.UpdateSlot(t_tempInventorySlot2.Item, t_tempInventorySlot2.Quantity);
            }
        }

        public void RemoveItem(Item pItem)
        {
            for (int index = 0; index < ListInventorySlots.Count; index++)
            {
                if (ListInventorySlots[index].Item == pItem)
                    ListInventorySlots[index].UpdateSlot(null, 0);
            }
        }

        public InventorySlot SetItemToFirstEmptySlot(Item pItem, int pQuantity)
        {
            for (int index = 0; index < ListInventorySlots.Count; index++)
            {
                if (ListInventorySlots[index].Item.Id <= -1)
                {
                    InventorySlot t_inventorySlot = ListInventorySlots[index];
                    t_inventorySlot.UpdateSlot(pItem, pQuantity);
                    return t_inventorySlot;
                }
            }

            // Set up for when inventory's full
            Debug.Log($"<size=22><color=orange>Inventory's Full</color></size>");
            return null;
        }

        void InitializeInventorySlots()
        {
            List<InventorySlot> t_tempListInventorySlots = null;
            if (ConstantAllowedItems)
                t_tempListInventorySlots = new List<InventorySlot>(ListInventorySlots);

            Container.ListInventorySlots = new List<InventorySlot>();

            for (int index = 0; index < DefaultInventorySize; index++)
            {
                InventorySlot t_inventorySlot = new InventorySlot();
                t_inventorySlot.Item.Id = -1;

                if (ConstantAllowedItems && t_tempListInventorySlots.Count > index)
                    t_inventorySlot.AllowedItems = t_tempListInventorySlots[index].AllowedItems;

                ListInventorySlots.Add(t_inventorySlot);
            }
        }

        #region Save, Load and Clear

        [ContextMenu("Save")]
        public void Save()
        {
            if (EncryptSaveFiles)
                SaveEncrypted();
            else
                SaveJSON();
        }

        void SaveEncrypted()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SavePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, Container);
            stream.Close();
        }

        void SaveJSON()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(SavePath);
            bf.Serialize(file, saveData);
            file.Close();
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(SavePath))
            {
                if (EncryptSaveFiles)
                    LoadEncrypted();
                else
                    LoadJSON();
            }
            else
            {
                InitializeInventorySlots();
                Save();
            }
        }

        void LoadEncrypted()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SavePath, FileMode.Open, FileAccess.Read);
            Inventory t_newContainer = (Inventory)formatter.Deserialize(stream);

            for (int index = 0; index < ListInventorySlots.Count; index++)
            {
                InventorySlot t_inventorySlot = ListInventorySlots[index];

                if (!ConstantAllowedItems)
                {
                    t_inventorySlot = t_newContainer.ListInventorySlots[index];
                }
                else
                {
                    t_inventorySlot.Parent = t_newContainer.ListInventorySlots[index].Parent;
                    t_inventorySlot.Item = t_newContainer.ListInventorySlots[index].Item;
                    t_inventorySlot.Quantity = t_newContainer.ListInventorySlots[index].Quantity;
                }

                ListInventorySlots[index].UpdateSlot(t_inventorySlot.Item, t_inventorySlot.Quantity);
            }

            stream.Close();
        }

        void LoadJSON()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SavePath, FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Clear();
        }

        [ContextMenu("Delete Save Data")]
        public void DeleteSaveData()
        {
            string[] t_filePaths = Directory.GetFiles(Application.persistentDataPath);
            foreach (string filePath in t_filePaths)
            {
                if (filePath.Contains(_saveFileName))
                {
                    File.Delete(filePath);
                    return;
                }
            }
        }

        #endregion
    }
}
