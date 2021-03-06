using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
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
    [SerializeField]
    Button _btn = null;

    [SerializeField, Space(15)]
    Image _imgPreview = null;

    public void UpdateValues(Sprite pSpBackground, Sprite pSpIcon, int pQuantity, bool pStackable)
    {
        _imgBackgrond.sprite = pSpBackground;
        _imgIcon.sprite = pSpIcon;
        _txtQuantity.text = pQuantity.ToString();

        UpdateImagePreview(pSpIcon);

        EnableDisable(true, pStackable);
    }

    void UpdateImagePreview(Sprite pIcon)
    {
        if (_imgPreview)
        {
            _imgPreview.sprite = pIcon;

            if (_imgPreview.sprite != null)
                _imgPreview.color = Color.white;
            else
                _imgPreview.color = Color.clear;
        }
    }

    public void Clear()
    {
        _imgBackgrond.sprite = null;
        _imgIcon.sprite = null;
        _txtQuantity.text = "";

        if (_imgPreview)
            _imgPreview.color = Color.clear;

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
