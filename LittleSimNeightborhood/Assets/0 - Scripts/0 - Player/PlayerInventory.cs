using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    SOInventory _soInventory = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            GroundItem t_groundItem = collision.GetComponent<GroundItem>();
            if (t_groundItem)
            {
                Item _item = new Item(t_groundItem.item);
                Debug.Log(_item.Id);
                _soInventory.AddItem(_item, 1);
                Destroy(collision.gameObject);
            }
        }
    }
}
