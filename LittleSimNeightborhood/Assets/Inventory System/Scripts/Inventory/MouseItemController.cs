using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class MouseItemController : MonoBehaviour
    {
        [SerializeField]
        RectTransform _rectMouseItem = null;

        //[HideInInspector]
        //public MouseData MouseItem;

        public void Initialize()
        {
            MouseData.RectTransform = _rectMouseItem;
            //MouseItem = new MouseData();
            //MouseData.RectTransform = _rectMouseItem;
        }
    }
}
