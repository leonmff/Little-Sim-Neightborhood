using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    List<SOItem> _listItemsToSell;

    public virtual void Interact() 
    {
        ShopController.instance.OpenShop(_listItemsToSell);
    }
}
