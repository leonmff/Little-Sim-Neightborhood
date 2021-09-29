using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _txtPlayerGold = null;

    SOVarInt _soPlayerGold;

    int _previousGold;

    private void Awake()
    {
        _soPlayerGold = Resources.Load<SOVarInt>("Player/PlayerGold");
        _previousGold = _soPlayerGold.Value;
    }

    private void Start()
    {
        _txtPlayerGold.text = _soPlayerGold.Value.ToString();
    }

    private void Update()
    {
        UpdateGoldHUD();
    }

    void UpdateGoldHUD()
    {
        if (_soPlayerGold.Value != _previousGold)
        {
            _txtPlayerGold.text = _soPlayerGold.Value.ToString();
            _previousGold = _soPlayerGold.Value;
        }
    }
}
