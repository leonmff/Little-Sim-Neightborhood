using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject _canvasInventory = null;

    InventoryController _inventoryControl;

    public static UnityAction OnCloseMenu;
    public static UnityAction OnOpenMenu;

    private void Awake()
    {
        InitializeInventory();
    }

    void InitializeInventory()
    {
        _inventoryControl = _canvasInventory.GetComponent<InventoryController>();
        if (_inventoryControl)
            _inventoryControl.InitializeSlots();
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
