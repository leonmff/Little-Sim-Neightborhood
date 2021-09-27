using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject _canvasInventory = null;

    public static UnityAction OnCloseMenu;
    public static UnityAction OnOpenMenu;

    private void OnEnable()
    {
        PlayerMenus.OnInventoryKeyPressed += OpenCloseInventory;
    }

    private void OnDisable()
    {
        PlayerMenus.OnInventoryKeyPressed -= OpenCloseInventory;
    }

    void OpenCloseInventory()
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
