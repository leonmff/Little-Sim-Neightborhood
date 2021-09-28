using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace InventorySystem
{
    public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
    {
        public SOItem Item;

        [SerializeField]
        SpriteRenderer _sr = null;

        public void OnBeforeSerialize()
        {
            if (_sr && Item)
            {
#if UNITY_EDITOR
                _sr.sprite = Item.UIIcon;
                EditorUtility.SetDirty(_sr);
#endif
            }   
        }

        public void OnAfterDeserialize() {}
    }
}
