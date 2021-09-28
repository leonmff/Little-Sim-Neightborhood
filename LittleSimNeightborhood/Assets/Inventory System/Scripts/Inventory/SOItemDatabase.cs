using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items Database")]
    public class SOItemDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        public SOItem[] ListItems;

        Dictionary<int, SOItem> _dicItems = new Dictionary<int, SOItem>();

        public SOItem GetItem(int pId) => _dicItems[pId];

        public void OnAfterDeserialize()
        {
            for (int index = 0; index < ListItems.Length; index++)
            {
                ListItems[index].Data.Id = index;
                _dicItems.Add(index, ListItems[index]);
            }
        }

        public void OnBeforeSerialize()
        {
            _dicItems = new Dictionary<int, SOItem>();
        }
    }
}
