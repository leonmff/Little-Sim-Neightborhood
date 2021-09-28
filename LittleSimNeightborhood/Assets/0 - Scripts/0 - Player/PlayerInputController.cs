using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class PlayerInputController : MonoBehaviour
{
    [System.Serializable]
    public struct InputPermissions
    {
        public bool CanMove;
        public bool CanJump;
        public bool CanInteract;
        public bool CanDoAction;
        public bool CanOpenCloseMenus;
        public bool CanGetInput;
    }

    [SerializeField]
    bool _getAxisRaw = false;

    [SerializeField, InspectorReadOnly, Space(15)]
    Vector2 _inputXY = Vector2.zero;
    [SerializeField, InspectorReadOnly]
    float _inputX = 0f;
    [SerializeField, InspectorReadOnly]
    float _inputY = 0f;

    [SerializeField, InspectorReadOnly, Header("Debug Variables")]
    InputPermissions _currentPermissions;
    [SerializeField]
    InputPermissions _previousPermissions;

    private void Start()
    {
        EnableAll();
        SaveOnPreviousPermissions();
    }

    private void OnEnable()
    {
        CanvasManager.OnOpenMenu += DisableAllKeepInventory;
        CanvasManager.OnCloseMenu += RestorePreviousPermissions;
    }

    private void OnDisable()
    {
        CanvasManager.OnOpenMenu -= DisableAllKeepInventory;
        CanvasManager.OnCloseMenu -= RestorePreviousPermissions;
    }

    public bool GetInputHorizontal(ref float pInputX)
    {
        if (!_currentPermissions.CanMove || !_currentPermissions.CanGetInput)
        {
            pInputX = 0f;
            _inputX = pInputX;
            return false;
        }

        pInputX = _getAxisRaw ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        _inputX = pInputX;
        return pInputX != 0;
    }

    public bool GetInputVertical(ref float pInputY)
    {
        if (!_currentPermissions.CanMove || !_currentPermissions.CanGetInput)
        {
            pInputY = 0f;
            _inputY = pInputY;
            return false;
        }

        pInputY = _getAxisRaw ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical");
        _inputY = pInputY;
        return pInputY != 0;
    }

    public bool GetInputHorizontalVertical(ref Vector2 pInputXY)
    {
        if (!_currentPermissions.CanMove || !_currentPermissions.CanGetInput)
        {
            pInputXY = Vector2.zero;
            _inputXY = pInputXY;
            return false;
        }

        float pInputX = _getAxisRaw ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        float pInputY = _getAxisRaw ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical");
        pInputXY = new Vector2(pInputX, pInputY);

        return pInputXY != Vector2.zero;
    }

    public bool GetInputJump()
    {
        if (!_currentPermissions.CanJump || !_currentPermissions.CanGetInput)
            return false;

        return Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetInputInteract()
    {
        if (!_currentPermissions.CanInteract || !_currentPermissions.CanGetInput)
            return false;

        return Input.GetButtonDown("Interact") || Input.GetKeyDown(KeyCode.E);
    }

    public bool GetInputFire()
    {
        //if (!_currentPermissions.CanDoAction || !_currentPermissions.CanGetInput)
         //   return false;

        return Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0);
    }

    public bool GetInputInventory()
    {
        if (!_currentPermissions.CanOpenCloseMenus || !_currentPermissions.CanGetInput)
            return false;

        return Input.GetButtonDown("Inventory") || Input.GetKeyDown(KeyCode.I);
    }

    public void DisableMovement()
    {
        _currentPermissions.CanMove = false;
        _currentPermissions.CanJump = false;
    }

    public void EnableMovement()
    {
        _currentPermissions.CanMove = true;
        _currentPermissions.CanJump = true;
    }

    public void DisableAllKeepInventory()
    {
        SaveOnPreviousPermissions();

        DisableMovement();
        _currentPermissions.CanInteract = false;
        _currentPermissions.CanDoAction = false;
        _currentPermissions.CanInteract = false;
    }

    public void EnableAll()
    {
        _currentPermissions.CanMove = true;
        _currentPermissions.CanJump = true;
        _currentPermissions.CanInteract = true;
        _currentPermissions.CanDoAction = true;
        _currentPermissions.CanOpenCloseMenus = true;
        _currentPermissions.CanGetInput = true;
    }

    public void DisableInput()
    {
        _currentPermissions.CanGetInput = false;
    }

    public void EnableInput()
    {
        _currentPermissions.CanGetInput = true;
    }

    void SaveOnPreviousPermissions()
    {
        _previousPermissions = _currentPermissions;
    }

    void RestorePreviousPermissions()
    {
        _currentPermissions = _previousPermissions;
    }
}
