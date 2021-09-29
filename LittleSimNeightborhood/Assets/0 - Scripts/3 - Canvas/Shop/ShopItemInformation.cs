using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

public class ShopItemInformation : MonoBehaviour
{
    [SerializeField]
    Image _imgIcon = null;
    [SerializeField]
    TextMeshProUGUI _txtName = null;
    [SerializeField]
    TextMeshProUGUI _txtPrice = null;

    Item _item;

    ShopController _shopController;
    public ShopController ShopController { set => _shopController = value; }

    public void UpdateInformation(SOItem pSOItem, Item pItem)
    {
        _item = pItem;
        _imgIcon.sprite = pSOItem.UIIcon;
        _txtName.text = pSOItem.Name;
        _txtPrice.text = pSOItem.Price.ToString();
    }

    public void BuyItem()
    {
        StartCoroutine(CoBuyItem());
    }

    IEnumerator CoBuyItem()
    {
        int t_price = int.Parse(_txtPrice.text);
        if (t_price <= _shopController.PlayerGold)
        {
            _shopController.BuyItem(t_price, _item);
        }
        else
        {
            yield return StartCoroutine(ConfirmationWindow.instance.CallConfirmation("Not enough gold."));

            //if (ConfirmationWindow.instance.Confirmed)
            //    _dicInventorySlots[pObject].RemoveItem();
        }

        yield break;
    }
}
