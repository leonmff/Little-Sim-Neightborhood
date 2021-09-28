using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotInformations : MonoBehaviour
{
    [SerializeField]
    Image _imgBackgrond = null;
    [SerializeField]
    GameObject _objSelected = null;
    [SerializeField]
    Image _imgIcon = null;
    [SerializeField]
    TextMeshProUGUI _txtQuantity = null;

    Button _btn;

    public void Initialize() => _btn = GetComponent<Button>();

    public void UpdateValues(Sprite pSpBackground, Sprite pSpIcon, int pQuantity, bool pStackable)
    {
        _imgBackgrond.sprite = pSpBackground;
        _imgIcon.sprite = pSpIcon;
        _txtQuantity.text = pQuantity.ToString();

        EnableDisable(true, pStackable);
    }

    public void Clear()
    {
        _imgBackgrond.sprite = null;
        _imgIcon.sprite = null;
        _txtQuantity.text = "";

        EnableDisable(false, false);
    }

    void EnableDisable(bool pValue, bool pStackable)
    {
        _imgBackgrond.gameObject.SetActive(pValue);
        _imgIcon.gameObject.SetActive(pValue);
        _txtQuantity.gameObject.SetActive(pStackable);
        _btn.enabled = pValue;
    }

    public void Select() => _objSelected.SetActive(true);

    public void Deselect() => _objSelected.SetActive(false);
}
