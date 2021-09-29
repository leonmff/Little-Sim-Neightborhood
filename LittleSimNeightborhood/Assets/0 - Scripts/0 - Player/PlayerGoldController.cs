using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldController : MonoBehaviour
{
    SOVarInt _soPlayerGold;

    public int GoldQuantity { get => _soPlayerGold.Value; }

    private void Awake()
    {
        _soPlayerGold = Resources.Load<SOVarInt>("Player/PlayerGold");
    }

    public void AddGold(int pQuantity) => _soPlayerGold.Value = _soPlayerGold.Value + pQuantity;

    public bool SpendGold(int pQuantity)
    {
        if (pQuantity <= _soPlayerGold.Value)
        {
            _soPlayerGold.Value = _soPlayerGold.Value - pQuantity;
            return true;
        }
        else
            return false;
    }
}
