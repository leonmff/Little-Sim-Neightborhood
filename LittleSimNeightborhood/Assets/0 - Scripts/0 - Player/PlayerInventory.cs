using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    SOInventory _soInventory = null;
    [SerializeField]
    SOInventory _soInventoryEquip = null;

    PlayerEquipController _equipControl;

    private void Awake()
    {
        _equipControl = GetComponentInChildren<PlayerEquipController>();
    }

    private void Start()
    {
        for (int index = 0; index < _soInventoryEquip.ListInventorySlots.Count; index++)
        {
            _soInventoryEquip.ListInventorySlots[index].OnBeforeUpdate += BeforeSlotUpdate;
            _soInventoryEquip.ListInventorySlots[index].OnAfterUpdate += AfterSlotUpdate;
        }

        //_soInventory.Load();
        //_soInventoryEquip.Load();

        StartCoroutine(DisplayEquippedItems());
    }

    IEnumerator DisplayEquippedItems()
    {
        yield return new WaitForEndOfFrame();

        for (int index = 0; index < _soInventoryEquip.ListInventorySlots.Count; index++)
        {
            InventorySlot t_inventorySlot = _soInventoryEquip.ListInventorySlots[index];
            _soInventoryEquip.ListInventorySlots[index].UpdateSlot(t_inventorySlot.Item, t_inventorySlot.Quantity);
        }
    }

    public void BeforeSlotUpdate(InventorySlot pSlot)
    {
        if (pSlot.SOItem == null)
            return;

        switch (pSlot.Parent.InventoryType)
        {
            case InventoryType.Inventory:
                break;
            case InventoryType.Equipment:
                //string t_debugMessage = string.Concat("REMOVED ''", pSlot.SOItem.name, "'' on ", pSlot.Parent.InventoryType, ", Allowed Items: ", string.Join(", ", pSlot.AllowedItems));
                //Debug.Log($"<size=22><color=red>{t_debugMessage}</color></size>");

                _equipControl.RemoveCloth(pSlot.SOItem);

                break;
            default:
                break;
        }

        //Debug.Log($"<size=22><color=orange>BEFORE Update</color></size>");
    }

    public void AfterSlotUpdate(InventorySlot pSlot)
    {
        if (pSlot.SOItem == null)
            return;

        switch (pSlot.Parent.InventoryType)
        {
            case InventoryType.Inventory:
                break;
            case InventoryType.Equipment:
                //string t_debugMessage = string.Concat("PLACED ''", pSlot.SOItem.name, "'' on ", pSlot.Parent.InventoryType, ", Allowed Items: ", string.Join(", ", pSlot.AllowedItems));
                //Debug.Log($"<size=22><color=aqua>{t_debugMessage}</color></size>");

                _equipControl.EquipCloth(pSlot.SOItem);
                break;
            default:
                break;
        }

        //Debug.Log($"<size=22><color=lime>AFTER Update</color></size>");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            GroundItem t_groundItem = collision.GetComponent<GroundItem>();
            if (t_groundItem)
            {
                Item t_item = new Item(t_groundItem.Item);

                if (_soInventory.AddItem(t_item, 1))
                    Destroy(collision.gameObject);
            }
        }
    }
}
