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
        public InventoryType InventoryType { get => _soInventory.TypeInventory; }

        [SerializeField]
        SOItemDatabase _soDatabase = null;

        public SOItemDatabase Database { get => _soDatabase; }

        [SerializeField, Space(7)]
        Transform _buttonPanels = null;

        [SerializeField, Space(7)]
        RectTransform _rectDragImage;
        [SerializeField]
        Image _imgDragItem;

        [SerializeField, Space(15)]
        GameObject _objShop = null;

        [SerializeField, InspectorReadOnly, Space(15)]
        List<InventorySlotInformations> _listInventorySlots;

        Dictionary<GameObject, InventorySlot> _dicInventorySlots;



        //protected void Awake() => _imgDragItem = MouseData.RectTransform.GetComponent<Image>();
        protected void Awake() => MouseData.RectTransform = _rectDragImage;

        public void InitializeSlots()
        {
            _dicInventorySlots = new Dictionary<GameObject, InventorySlot>();
            _listInventorySlots = new List<InventorySlotInformations>();

            for (int index = 0; index < _buttonPanels.childCount; index++)
            {
                GameObject t_objButton = _buttonPanels.GetChild(index).gameObject;

                InventorySlotInformations t_inventorySlot = t_objButton.GetComponent<InventorySlotInformations>();
                if (t_inventorySlot)
                    _listInventorySlots.Add(t_inventorySlot);

                AddEvent(t_objButton, EventTriggerType.PointerEnter, delegate { OnHoverOverObject(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.PointerExit, delegate { OnExit(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.BeginDrag, delegate { OnDragStart(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.EndDrag, delegate { OnDragEnd(t_objButton); });
                AddEvent(t_objButton, EventTriggerType.Drag, delegate { OnDrag(t_objButton); });

                _soInventory.ListInventorySlots[index].SlotDisplay = t_objButton;
                _dicInventorySlots.Add(t_objButton, _soInventory.Container.ListInventorySlots[index]);
            }

            AddEvent(_objShop, EventTriggerType.PointerEnter, delegate { OnHoverOverObject(_objShop); });
            AddEvent(_objShop, EventTriggerType.PointerExit, delegate { OnExit(_objShop); });

            LoadInventory();
        }

        protected void OnEnable() => LoadInventory();

        protected void LoadInventory()
        {
            //Debug.Log($"<size=22><color=lime>Passing load data to inventory ui</color></size>");

            for (int index = 0; index < _soInventory.Container.ListInventorySlots.Count; index++)
            {
                if (index < _listInventorySlots.Count)
                {
                    InventorySlot t_inventorySlot = _soInventory.Container.ListInventorySlots[index];
                    t_inventorySlot.Parent = (InventoryController)this;
                    t_inventorySlot.OnAfterUpdate += SlotUpdate;

                    if (t_inventorySlot.Item.Id <= -1)
                        continue;

                    SOItem t_item = _soDatabase.ListItems[t_inventorySlot.Item.Id];
                    _listInventorySlots[index].UpdateValues(t_item.UIBackground, t_item.UIIcon, t_inventorySlot.Quantity, t_item.Stackable);
                }
            }
        }

        void SlotUpdate(InventorySlot pSlot)
        {
            if (pSlot.Item.Id >= 0)
            {
                SOItem t_soItem = pSlot.SOItem;
                InventorySlotInformations t_inventorySlotInfo = pSlot.SlotDisplay.GetComponent<InventorySlotInformations>();

                t_inventorySlotInfo.UpdateValues(t_soItem.UIBackground, t_soItem.UIIcon, pSlot.Quantity, t_soItem.Stackable);
            }
            else if (pSlot.SlotDisplay != null)
            {
                InventorySlotInformations t_inventorySlotInfo = pSlot.SlotDisplay.GetComponent<InventorySlotInformations>();
                t_inventorySlotInfo.Clear();
            }

            if (_soInventory.AutoSaveOnInventory)
                _soInventory.Save();
        }

        public void UpdateInventoryInformation()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> t_slot in _dicInventorySlots)
            {
                if (t_slot.Value.Item.Id >= 0)
                {
                    SOItem t_soItem = t_slot.Value.SOItem; 
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

        public void OnHoverOverObject(GameObject pObjectHovered)
        {
            MouseData.SlotHovered = pObjectHovered;

            if (_dicInventorySlots.ContainsKey(pObjectHovered))
                MouseData.InventorySlotHovered = _dicInventorySlots[pObjectHovered];
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
            if (!MouseData.SlotHovered)
            {
                yield return StartCoroutine(ConfirmationWindow.instance.CallConfirmation("The item will be destroyed."));

                if (ConfirmationWindow.instance.Confirmed)
                    _dicInventorySlots[pObject].RemoveItem();
            }
            else
            {
                if (MouseData.SlotHovered.CompareTag("Shop"))
                {
                    ShopController t_shopControl = MouseData.SlotHovered.GetComponentInParent<ShopController>();

                    InventorySlot t_inventorySlot = _dicInventorySlots[pObject];
                    int t_sellPrice = Mathf.FloorToInt((t_inventorySlot.SOItem.Price * (t_inventorySlot.Item.Stackable ? t_inventorySlot.Quantity : 1f)) * 0.7f);

                    yield return StartCoroutine(ConfirmationWindow.instance.CallConfirmation(string.Concat("Sell it for ", t_sellPrice)));

                    if (ConfirmationWindow.instance.Confirmed)
                    {
                        _dicInventorySlots[pObject].RemoveItem();
                        t_shopControl.SellItem(t_sellPrice);
                    }
                }
                else
                {
                    Debug.Log($"<size=22><color=aqua>SlotHovered: {MouseData.SlotHovered}</color></size>");

                    bool t_canReplace = MouseData.InventorySlotHovered.CanPlaceInSlot(_soInventory.database.ListItems[_dicInventorySlots[pObject].Item.Id]);
                    bool t_hasEmptyID = MouseData.InventorySlotHovered.Item.Id <= -1;
                    bool t_idOcupied = MouseData.InventorySlotHovered.Item.Id >= 0;

                    if (t_canReplace && (t_hasEmptyID || t_idOcupied))
                        _soInventory.SwapItem(_dicInventorySlots[pObject], MouseData.InventorySlotHovered.Parent._dicInventorySlots[MouseData.SlotHovered]);
                }
            }

            _imgDragItem.sprite = null;
            _imgDragItem.color = Color.clear;

            yield return null;
        }

        protected void OnExit(GameObject pObject)
        {
            MouseData.SlotHovered = null;
            MouseData.InventorySlotHovered = null;
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
