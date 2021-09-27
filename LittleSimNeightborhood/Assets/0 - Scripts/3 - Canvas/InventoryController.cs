using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    SOInventory _soInventory = null;
    [SerializeField]
    SOItemDatabase _soDatabase = null;

    [SerializeField, Space(7)]
    Transform _buttonPanels = null;

    [SerializeField, InspectorReadOnly, Space(15)]
    List<InventorySlotInformations> _listInventorySlots;

    private void Awake()
    {
        _listInventorySlots = new List<InventorySlotInformations>();

        for (int index = 0; index < _buttonPanels.childCount; index++)
        {
            InventorySlotInformations t_inventorySlot = _buttonPanels.GetChild(index).GetComponent<InventorySlotInformations>();
            if (t_inventorySlot)
                _listInventorySlots.Add(t_inventorySlot);
        }
    }

    private void OnEnable()
    {
        LoadInventory();
    }

    void LoadInventory()
    {
        //List<SOItemOld> t_listItems = new List<SOItemOld>(PlayerInfo.instance.ListItems);
        //Debug.Log($"Count {t_listItems.Count}");

        for (int index = 0; index < _soInventory.Container.ListItems.Count; index++)
        {
            if (index < _listInventorySlots.Count)
            {
                InventorySlot t_inventorySlot = _soInventory.Container.ListItems[index];
                if (t_inventorySlot.ID <= -1)
                    continue;

                SOItem t_item = _soDatabase.GetItem(t_inventorySlot.ID);
                _listInventorySlots[index].SetValues(t_item.UIBackground, t_item.UIIcon, t_inventorySlot.Quantity, t_item.Stackable);
            }
        }
    }
}
