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
