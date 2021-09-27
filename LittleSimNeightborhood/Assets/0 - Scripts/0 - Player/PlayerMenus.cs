using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMenus : MonoBehaviour
{
    public static UnityAction OnInventoryKeyPressed;

    PlayerInputController _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInputController>();
    }

    private void Update()
    {
        CheckInputToOpenInventory();
    }

    void CheckInputToOpenInventory()
    {
        if (_playerInput.GetInputInventory())
        {
            OnInventoryKeyPressed?.Invoke();
        }
    }
}
