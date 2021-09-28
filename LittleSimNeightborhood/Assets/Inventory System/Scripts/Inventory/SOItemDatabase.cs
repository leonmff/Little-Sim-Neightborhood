using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items Database")]
    public class SOItemDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        public SOItem[] ListItems;

        public void OnAfterDeserialize()
        {
            for (int index = 0; index < ListItems.Length; index++)
            {
                ListItems[index].Data.Id = index;
            }
        }

        public void OnBeforeSerialize(){}
    }
}
