using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    bool _getAxisRaw = false;

    [SerializeField, InspectorReadOnly, Space(15)]
    Vector2 _inputXY = Vector2.zero;
    [SerializeField, InspectorReadOnly]
    float _inputX = 0f;
    [SerializeField, InspectorReadOnly]
    float _inputY = 0f;

    [SerializeField, InspectorReadOnly, Header("Debug Variables")]
    bool _canMove;
    [SerializeField, InspectorReadOnly]
    bool _canJump;
    [SerializeField, InspectorReadOnly]
    bool _canInteract;

    private void Start()
    {
        _canMove = true;
        _canJump = true;
        _canInteract = true;
    }

    public bool GetInputHorizontal(ref float pInputX)
    {
        if (!_canMove)
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
        if (!_canMove)
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
        if (!_canMove)
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
        if (!_canJump)
            return false;

        return Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetInputInteract()
    {
        if (!_canInteract)
            return false;

        return Input.GetButtonDown("Interact") || Input.GetKeyDown(KeyCode.E);
    }

    public bool GetInputFire()
    {
        if (!_canInteract)
            return false;

        return Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0);
    }

    public void DisableMovement()
    {
        _canMove = false;
        _canJump = false;
    }

    public void EnableMovement()
    {
        _canMove = true;
        _canJump = true;
    }

    public void DisableInteractions()
    {
        _canInteract = false;
    }

    public void EnableInteractions()
    {
        _canInteract = true;
    }

    public void DisableInputs()
    {
        DisableMovement();
        DisableInteractions();
    }

    public void EnableInputs()
    {
        EnableMovement();
        EnableInteractions();
    }
}
