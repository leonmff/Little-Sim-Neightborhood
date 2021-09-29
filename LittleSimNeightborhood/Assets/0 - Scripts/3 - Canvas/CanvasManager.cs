using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField]
        GameObject _canvasInventory = null;

        [SerializeField, Space(7)]
        InventoryItems _inventoryControl = null;
        [SerializeField]
        InventoryEquips _inventoryEquipControl = null;

        public static UnityAction OnCloseMenu;
        public static UnityAction OnOpenMenu;

        private void Awake()
        {
            InitializeInventories();
        }

        void InitializeInventories()
        {
            _inventoryControl.InitializeSlots();
            _inventoryEquipControl.InitializeSlots();
        }

        private void OnEnable()
        {
            PlayerMenus.OnInventoryKeyPressed += OpenCloseInventory;
        }

        private void OnDisable()
        {
            PlayerMenus.OnInventoryKeyPressed -= OpenCloseInventory;
        }

        public void OpenCloseInventory()
        {
            if (_canvasInventory.activeInHierarchy)
            {
                _canvasInventory.SetActive(false);
                OnCloseMenu?.Invoke();
            }
            else
            {
                _canvasInventory.SetActive(true);
                OnOpenMenu?.Invoke();
            }
        }
    }
}
