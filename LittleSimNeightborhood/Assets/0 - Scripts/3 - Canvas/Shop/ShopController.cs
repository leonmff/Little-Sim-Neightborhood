using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopController : MonoBehaviour
{
    public static ShopController instance;

    [SerializeField]
    SOInventory _soInventory = null;

    [SerializeField, Space(7)]
    GameObject _objShop = null;
    [SerializeField]
    Transform _shopItemsParent = null;

    [SerializeField, Space(7)]
    GameObject _objInventory = null;

    [SerializeField, Space(15)]
    List<ShopItemInformation> _listShopInformation;

    List<SOItem> _listItemsToSell;

    SOVarInt _soPlayerGold;
    public int PlayerGold { get => _soPlayerGold.Value; }

    public static UnityAction OnShopOpened;
    public static UnityAction OnShopClosed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        _soPlayerGold = Resources.Load<SOVarInt>("Player/PlayerGold");

        FillLists();
    }

    void FillLists()
    {
        _listShopInformation = new List<ShopItemInformation>();

        for (int index = 0; index < _shopItemsParent.childCount; index++)
        {
            ShopItemInformation t_shopInformation = _shopItemsParent.GetChild(index).GetComponent<ShopItemInformation>();
            if (t_shopInformation)
            {
                t_shopInformation.ShopController = this;
                _listShopInformation.Add(t_shopInformation);
            }

            _shopItemsParent.GetChild(index).gameObject.SetActive(false);
        }
    }

    public void OpenShop(List<SOItem> pListSOItemsToSell)
    {
        _listItemsToSell = new List<SOItem>(pListSOItemsToSell);
        UpdateSlotsInformations();
        _objShop.SetActive(true);
        _objInventory.SetActive(true);

        StartCoroutine(CloseWithInventory());

        OnShopOpened?.Invoke();
    }

    public void BuyItem(int pPrice, Item pItem)
    {
        _soPlayerGold.Value = _soPlayerGold.Value - pPrice;
        _soInventory.AddItem(pItem, 1);
    }

    public void SellItem(int pPrice)
    {
        _soPlayerGold.Value = _soPlayerGold.Value + pPrice;
    }

    void UpdateSlotsInformations()
    {
        for (int index = 0; index < _listItemsToSell.Count; index++)
        {
            SOItem t_soItem = _listItemsToSell[index];
            Item t_item = new Item(t_soItem);
            
            _listShopInformation[index].UpdateInformation(t_soItem, t_item);
            _listShopInformation[index].gameObject.SetActive(true);
        }
    }

    public void CloseShopAndInventory()
    {
        _objShop.SetActive(false);
        _objInventory.SetActive(false);

        OnShopClosed?.Invoke();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator CloseWithInventory()
    {
        yield return new WaitWhile(() => _objInventory.activeInHierarchy);
        _objShop.SetActive(false);
    }

    public void OnHover()
    {
        Debug.Log($"Hovering me!");
    }

    public void OnExit()
    {
        Debug.Log($"Hovering me stoped!");
    }
}
