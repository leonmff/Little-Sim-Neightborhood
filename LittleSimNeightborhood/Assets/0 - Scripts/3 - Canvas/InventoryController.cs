using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    SOInventory _soInventory = null;
    [SerializeField]
    SOItemDatabase _soDatabase = null;

    [SerializeField, Space(7)]
    Transform _buttonPanels = null;

    [SerializeField, Space(7)]
    Transform _tfDragItemImage = null;

    Image _imgDragItem;

    [SerializeField, InspectorReadOnly, Space(15)]
    List<InventorySlotInformations> _listInventorySlots;

    MouseItem _mouseItem;

    Dictionary<GameObject, InventorySlot> _dicInventorySlots;

    private void Awake()
    {
        _imgDragItem = _tfDragItemImage.GetComponent<Image>();

        _mouseItem = new MouseItem();
        _mouseItem.RectTransform = _tfDragItemImage.GetComponent<RectTransform>();
    }

    public void InitializeSlots()
    {
        _dicInventorySlots = new Dictionary<GameObject, InventorySlot>();
        _listInventorySlots = new List<InventorySlotInformations>();

        for (int index = 0; index < _buttonPanels.childCount; index++)
        {
            GameObject t_objButton = _buttonPanels.GetChild(index).gameObject;

            InventorySlotInformations t_inventorySlot = t_objButton.GetComponent<InventorySlotInformations>();
            if (t_inventorySlot)
            {
                t_inventorySlot.Initialize();
                _listInventorySlots.Add(t_inventorySlot);
            }

            AddEvent(t_objButton, EventTriggerType.PointerEnter, delegate { OnEnter(t_objButton); });
            AddEvent(t_objButton, EventTriggerType.PointerExit, delegate { OnExit(t_objButton); });
            AddEvent(t_objButton, EventTriggerType.BeginDrag, delegate { OnDragStart(t_objButton); });
            AddEvent(t_objButton, EventTriggerType.EndDrag, delegate { OnDragEnd(t_objButton); });
            AddEvent(t_objButton, EventTriggerType.Drag, delegate { OnDrag(t_objButton); });

            _dicInventorySlots.Add(t_objButton, _soInventory.Container.ListItems[index]);
        }
    }

    private void OnEnable() => LoadInventory();

    void LoadInventory()
    {
        for (int index = 0; index < _soInventory.Container.ListItems.Count; index++)
        {
            if (index < _listInventorySlots.Count)
            {
                InventorySlot t_inventorySlot = _soInventory.Container.ListItems[index];
                if (t_inventorySlot.ID <= -1)
                    continue;

                SOItem t_item = _soDatabase.GetItem(t_inventorySlot.ID);
                _listInventorySlots[index].UpdateValues(t_item.UIBackground, t_item.UIIcon, t_inventorySlot.Quantity, t_item.Stackable);
            }
        }
    }

    void UpdateInventoryInformation()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> t_slot in _dicInventorySlots)
        {
            if (t_slot.Value.ID >= 0)
            {
                SOItem t_soItem = _soDatabase.ListItems[t_slot.Value.Item.Id];
                InventorySlotInformations t_inventorySlotInfo = t_slot.Key.GetComponent<InventorySlotInformations>();

                t_inventorySlotInfo.UpdateValues(t_soItem.UIBackground, t_soItem.UIIcon, t_slot.Value.Quantity, t_soItem.Stackable);
            }
            else
            {
                InventorySlotInformations t_inventorySlotInfo = t_slot.Key.GetComponent<InventorySlotInformations>();
                t_inventorySlotInfo.Clear();
            }
        }

        _soInventory.Save();
    }

    public void OnEnter(GameObject pObject)
    {
        _mouseItem.ObjectHovered = pObject;

        if (_dicInventorySlots.ContainsKey(pObject))
            _mouseItem.InventorySlotHovered = _dicInventorySlots[pObject];
    }

    private void OnDragStart(GameObject pObject)
    {
        InventorySlot t_inventorySlot = _dicInventorySlots[pObject];
        if (t_inventorySlot.ID >= 0)
        {
            _imgDragItem.sprite = _soDatabase.ListItems[t_inventorySlot.ID].UIIcon;
            _imgDragItem.color = Color.white;

            _mouseItem.InventorySlot = t_inventorySlot;
        }
        else
            _imgDragItem.color = Color.clear;
    }

    private void OnDrag(GameObject pObject)
    {
        if (_mouseItem.InventorySlot != null)
            _mouseItem.RectTransform.position = Input.mousePosition;
    }

    private void OnDragEnd(GameObject pObject)
    {
        if (_mouseItem.ObjectHovered)
            _soInventory.SwapItem(_dicInventorySlots[pObject], _dicInventorySlots[_mouseItem.ObjectHovered]);
        else
            _soInventory.RemoveItem(_dicInventorySlots[pObject].Item);

        _imgDragItem.sprite = null;
        _imgDragItem.color = Color.clear;

        UpdateInventoryInformation();
    }

    private void OnExit(GameObject pObject)
    {
        _mouseItem.ObjectHovered = null;
        _mouseItem.InventorySlotHovered = null;
    }

    void AddEvent(GameObject pObject, EventTriggerType pTriggerType, UnityAction<BaseEventData> pAction)
    {
        EventTrigger t_eventTrigger = pObject.GetComponent<EventTrigger>();

        EventTrigger.Entry t_eventTriggerEntry = new EventTrigger.Entry();

        t_eventTriggerEntry.eventID = pTriggerType;
        t_eventTriggerEntry.callback.AddListener(pAction);

        t_eventTrigger.triggers.Add(t_eventTriggerEntry);
    }
}
