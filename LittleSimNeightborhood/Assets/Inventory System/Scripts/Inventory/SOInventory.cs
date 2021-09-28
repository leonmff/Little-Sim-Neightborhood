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
        public SOItemDatabase database;

        [Space(7)]
        public bool EncryptSaveFiles = true;
        public bool AutoLoadOnAwake = true;
        public bool AutoSaveOnInventory = true;

        [Space(7)]
        public int DefaultInventorySize;

        [Space(7)]
        public Inventory Container;

        const string SAVE_FILE_NAME = "inventorySave";
        string SavePath { get => string.Concat(Application.persistentDataPath, "/", SAVE_FILE_NAME); }

        private void OnEnable()
        {
            if (AutoLoadOnAwake)
                Load();
        }

        public void AddItem(Item pItem, int pQuantity)
        {
            if (!pItem.Stackable)
                SetItemToFirstEmptySlot(pItem, pQuantity);
            else
            {
                InventorySlot t_inventorySlot = Container.ListItems.Find(t_element => t_element.ID == pItem.Id);
                if (t_inventorySlot != null)
                    t_inventorySlot.AddAmount(pQuantity);
                else
                    SetItemToFirstEmptySlot(pItem, pQuantity);
            }

            if (AutoSaveOnInventory)
                Save();
        }

        public void SwapItem(InventorySlot pInventorySlot1, InventorySlot pInventorySlot2)
        {
            InventorySlot t_tempInventorySlot2 = new InventorySlot(pInventorySlot2.ID, pInventorySlot2.Item, pInventorySlot2.Quantity);
            pInventorySlot2.UpdateSlot(pInventorySlot1.ID, pInventorySlot1.Item, pInventorySlot1.Quantity);
            pInventorySlot1.UpdateSlot(t_tempInventorySlot2.ID, t_tempInventorySlot2.Item, t_tempInventorySlot2.Quantity);
        }

        public void RemoveItem(Item pItem)
        {
            for (int index = 0; index < Container.ListItems.Count; index++)
            {
                if (Container.ListItems[index].Item == pItem)
                    Container.ListItems[index].UpdateSlot(-1, null, 0);
            }
        }

        public InventorySlot SetItemToFirstEmptySlot(Item pItem, int pQuantity)
        {
            for (int index = 0; index < Container.ListItems.Count; index++)
            {
                if (Container.ListItems[index].ID <= -1)
                {
                    InventorySlot t_inventorySlot = Container.ListItems[index];
                    t_inventorySlot.UpdateSlot(pItem.Id, pItem, pQuantity);
                    return t_inventorySlot;
                }
            }

            // Set up for when inventory's full
            Debug.Log($"<size=22><color=orange>Inventory's Full</color></size>");
            return null;
        }

        void InitializeInventorySlots()
        {
            Container.ListItems = new List<InventorySlot>();

            for (int index = 0; index < DefaultInventorySize; index++)
            {
                InventorySlot t_inventorySlot = new InventorySlot();
                t_inventorySlot.ID = -1;

                Container.ListItems.Add(t_inventorySlot);
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

            for (int index = 0; index < Container.ListItems.Count; index++)
            {
                InventorySlot t_inventorySlot = t_newContainer.ListItems[index];
                Container.ListItems[index].UpdateSlot(t_inventorySlot.ID, t_inventorySlot.Item, t_inventorySlot.Quantity);
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
            Container = new Inventory();
        }

        [ContextMenu("Delete Save Data")]
        public void DeleteSaveData()
        {
            string[] t_filePaths = Directory.GetFiles(Application.persistentDataPath);
            foreach (string filePath in t_filePaths)
            {
                if (filePath.Contains(SAVE_FILE_NAME))
                {
                    File.Delete(filePath);
                    return;
                }
            }
        }

        #endregion
    }
}
