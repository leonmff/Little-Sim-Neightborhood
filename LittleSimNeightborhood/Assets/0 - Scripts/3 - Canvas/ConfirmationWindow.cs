using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using TMPro;

public class ConfirmationWindow : MonoBehaviour
{
    public static ConfirmationWindow instance;

    [SerializeField]
    GameObject _objConfirmationWindow = null;

    [SerializeField, Space(7)]
    TextMeshProUGUI _txtMessage = null;

    [SerializeField, Space(7)]
    Button _btnConfirm = null;

    TextMeshProUGUI _txtBtnConfirm;

    [SerializeField]
    Button _btnCancel = null;

    TextMeshProUGUI _txtBtnCancel;

    bool _confirmed;
    public bool Confirmed { get => _confirmed; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(this);

        _txtBtnConfirm = _btnConfirm.GetComponentInChildren<TextMeshProUGUI>();
        _txtBtnCancel = _btnCancel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public IEnumerator CallConfirmation(string pTxtBtnConfirm, string pTxtBtnCancel, string pTxtMessage)
    {
        _confirmed = false;

        _txtBtnConfirm.text = pTxtBtnConfirm.Trim() != "" ? pTxtBtnConfirm.Trim() : "CONFIRM";
        _txtBtnCancel.text = pTxtBtnCancel.Trim() != "" ? pTxtBtnCancel.Trim() : "CANCEL";
        _txtMessage.text = pTxtMessage.Trim() != "" ? pTxtMessage.Trim() : "Do you wish to procced?";

        yield return StartCoroutine(Confirmation());
    }

    public IEnumerator CallConfirmation(string pTxtMessage)
    {
        _confirmed = false;

        _txtBtnConfirm.text = "CONFIRM";
        _txtBtnCancel.text = "CANCEL";
        _txtMessage.text = pTxtMessage.Trim() != "" ? pTxtMessage.Trim() : "Do you wish to procced?";

        yield return StartCoroutine(Confirmation());
    }

    public IEnumerator CallConfirmation()
    {
        _confirmed = false;

        _txtBtnConfirm.text = "CONFIRM";
        _txtBtnCancel.text = "CANCEL";
        _txtMessage.text = "Do you wish to procced?";

        yield return StartCoroutine(Confirmation());
    }

    IEnumerator Confirmation()
    {
        _objConfirmationWindow.SetActive(true);

        WaitForUIButtons t_waitForButton = new WaitForUIButtons(_btnConfirm, _btnCancel);

        yield return t_waitForButton.Reset();

        _confirmed = (t_waitForButton.PressedButton == _btnConfirm);

        _objConfirmationWindow.SetActive(false);
    }
}
