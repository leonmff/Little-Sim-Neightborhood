using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InventorySystem
{
    public abstract class InventoryController : MonoBehaviour
    {
        [SerializeField]
        SOInventory _soInventory = null;

        [SerializeField]
        SOItemDatabase _soDatabase = null;

        public SOItemDatabase Database { get => _soDatabase; }

        [SerializeField, Space(7)]
        Transform _buttonPanels = null;

        [SerializeField, InspectorReadOnly, Space(15)]
        List<InventorySlotInformations> _listInventorySlots;

        Dictionary<GameObject, InventorySlot> _dicInventorySlots;

        Image _imgDragItem;

        protected void Awake()
        {
            _imgDragItem = MouseData.RectTransform.GetComponent<Image>();
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
                    _listInventorySlots.Add(t_inventorySlot);
                }

                AddEvent(t_objButton, EventTriggerType.PointerEnter, delegate { OnEnter(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.PointerExit, delegate { OnExit(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.BeginDrag, delegate { OnDragStart(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.EndDrag, delegate { OnDragEnd(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.Drag, delegate { OnDrag(t_objButton); });

                _dicInventorySlots.Add(t_objButton, _soInventory.Container.ListItems[index]);
            }

            LoadInventory();
        }

        protected void OnEnable() => LoadInventory();

        protected void LoadInventory()
        {
            for (int index = 0; index < _soInventory.Container.ListItems.Count; index++)
            {
                if (index < _listInventorySlots.Count)
                {
                    InventorySlot t_inventorySlot = _soInventory.Container.ListItems[index];
                    t_inventorySlot.Parent = (InventoryController)this;

                    if (t_inventorySlot.Item.Id <= -1)
                        continue;

                    SOItem t_item = _soDatabase.GetItem(t_inventorySlot.Item.Id);
                    _listInventorySlots[index].UpdateValues(t_item.UIBackground, t_item.UIIcon, t_inventorySlot.Quantity, t_item.Stackable);
                }
            }
        }

        public void UpdateInventoryInformation()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> t_slot in _dicInventorySlots)
            {
                if (t_slot.Value.Item.Id >= 0)
                {
                    SOItem t_soItem = t_slot.Value.SOItem; //_soDatabase.ListItems[t_slot.Value.Item.Id];
                    InventorySlotInformations t_inventorySlotInfo = t_slot.Key.GetComponent<InventorySlotInformations>();

                    t_inventorySlotInfo.UpdateValues(t_soItem.UIBackground, t_soItem.UIIcon, t_slot.Value.Quantity, t_soItem.Stackable);
                }
                else
                {
                    InventorySlotInformations t_inventorySlotInfo = t_slot.Key.GetComponent<InventorySlotInformations>();
                    t_inventorySlotInfo.Clear();
                }
            }

            if (_soInventory.AutoSaveOnInventory)
                _soInventory.Save();
        }

        public void OnEnter(GameObject pObject)
        {
            MouseData.SlotHovered = pObject;

            if (_dicInventorySlots.ContainsKey(pObject))
                MouseData.InventorySlotHovered = _dicInventorySlots[pObject];
        }

        protected void OnDragStart(GameObject pObject)
        {
            InventorySlot t_inventorySlot = _dicInventorySlots[pObject];
            if (t_inventorySlot.Item.Id >= 0)
            {
                _imgDragItem.sprite = _dicInventorySlots[pObject].SOItem.UIIcon; //_soDatabase.ListItems[t_inventorySlot.Item.Id].UIIcon;
                _imgDragItem.color = Color.white;

                MouseData.InventorySlot = t_inventorySlot;
            }
            else
                _imgDragItem.color = Color.clear;
        }

        protected void OnDrag(GameObject pObject)
        {
            if (MouseData.InventorySlot != null)
                MouseData.RectTransform.position = Input.mousePosition;
        }

        protected void OnDragEnd(GameObject pObject) => StartCoroutine(CoDragEnd(pObject));

        protected IEnumerator CoDragEnd(GameObject pObject)
        {
            //var itemOnMouse = _mouseItemControl.MouseItem;
            //var mouseHoverItem = itemOnMouse.InventorySlotHovered;
            //var mouseHoverObj = itemOnMouse.SlotHovered;

            //if (mouseHoverObj)
            //{
            //    if (mouseHoverItem.CanPlaceInSlot(_soInventory.database.GetItem(_dicInventorySlots[pObject].ID)) && (mouseHoverItem.Item.Id <= -1 || (mouseHoverItem.Item.Id >= 0 && _dicInventorySlots[pObject].CanPlaceInSlot(_soInventory.database.GetItem(mouseHoverItem.Item.Id)))))
            //        _soInventory.SwapItem(_dicInventorySlots[pObject], mouseHoverItem.Parent._dicInventorySlots[mouseHoverObj]);
            //}
            //else
            //{
            //    yield return StartCoroutine(ConfirmationWindow.instance.CallConfirmation("The item will be destroyed."));

            //    if (ConfirmationWindow.instance.Confirmed)
            //        _soInventory.RemoveItem(_dicInventorySlots[pObject].Item);
            //}

            if (!MouseData.SlotHovered)
            {
                yield return StartCoroutine(ConfirmationWindow.instance.CallConfirmation("The item will be destroyed."));

                if (ConfirmationWindow.instance.Confirmed)
                    _dicInventorySlots[pObject].RemoveItem();
            }
            else
            {
                bool t_canReplace = MouseData.InventorySlotHovered.CanPlaceInSlot(_soInventory.database.GetItem(_dicInventorySlots[pObject].Item.Id));
                bool t_hasEmptyID = MouseData.InventorySlotHovered.Item.Id <= -1;
                bool t_idOcupied = MouseData.InventorySlotHovered.Item.Id >= 0;
                //bool t_canReplaceInverse = _dicInventorySlots[pObject].CanPlaceInSlot(_soInventory.database.GetItem(MouseData.InventorySlotHovered.Item.Id));
                
                if (t_canReplace && (t_hasEmptyID || (t_idOcupied)))// && t_canReplaceInverse)))
                    _soInventory.SwapItem(_dicInventorySlots[pObject], MouseData.InventorySlotHovered.Parent._dicInventorySlots[MouseData.SlotHovered]);

                //if (MouseData.InventorySlotHovered.CanPlaceInSlot(_soInventory.database.GetItem(_dicInventorySlots[pObject].ID)) && (mouseHoverItem.Item.Id <= -1 || (mouseHoverItem.Item.Id >= 0 && _dicInventorySlots[pObject].CanPlaceInSlot(_soInventory.database.GetItem(mouseHoverItem.Item.Id)))))
                //    _soInventory.SwapItem(_dicInventorySlots[pObject], mouseHoverItem.Parent._dicInventorySlots[mouseHoverObj]);

                //InventorySlot t_inventorySlotHovered = MouseData.InventoryControl._dicInventorySlots[MouseData.SlotHovered];
                //_soInventory.SwapItem(_dicInventorySlots[pObject], t_inventorySlotHovered);
            }

            _imgDragItem.sprite = null;
            _imgDragItem.color = Color.clear;

            UpdateInventoryInformation();

            yield return null;
        }

        protected void OnExit(GameObject pObject)
        {
            MouseData.SlotHovered = null;
            MouseData.InventorySlotHovered = null;
            //_mouseItemControl.MouseItem.SlotHovered = null;
            //_mouseItemControl.MouseItem.InventorySlotHovered = null;
        }

        protected void AddEvent(GameObject pObject, EventTriggerType pTriggerType, UnityAction<BaseEventData> pAction)
        {
            EventTrigger t_eventTrigger = pObject.GetComponent<EventTrigger>();

            EventTrigger.Entry t_eventTriggerEntry = new EventTrigger.Entry();

            t_eventTriggerEntry.eventID = pTriggerType;
            t_eventTriggerEntry.callback.AddListener(pAction);

            t_eventTrigger.triggers.Add(t_eventTriggerEntry);
        }
    }
}
