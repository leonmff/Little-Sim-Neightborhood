using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Debug;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    bool _showDebug = false;

    [SerializeField, Space(7)]
    Transform _transfHeadTop = null;

    [SerializeField, Space(7)]
    float _sizeDetection = 0f;

    Vector3 Pos { get => _transfHeadTop.position; }

    Vector3 _direction;

    PlayerMovement _playerMovement;
    PlayerInputController _playerInput;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInputController>();
    }

    void Update()
    {
        UpdateDirection();

        if (_playerInput.GetInputInteract())
            Interact();
    }

    void Interact()
    {
        Vector3 t_pos = Pos + new Vector3(_direction.x / 2f, _direction.y);

        Collider2D t_collider = Physics2D.OverlapBox(t_pos, new Vector2(_sizeDetection, _sizeDetection), 0f, LayerMask.GetMask("Interactable"));
        if (t_collider)
        {
            IInteractable t_interactable = t_collider.GetComponent<IInteractable>();
            if (t_interactable != null)
                t_interactable.Interact();
        }

        if (_showDebug)
            DebugCustom.DrawSquare(t_pos, _sizeDetection, Color.red, 0.25f);
    }

    void UpdateDirection()
    {
        if (_playerMovement.Direction != Vector2.zero)
        {
            _direction = _playerMovement.Direction;
            _direction.y = _direction.y > 0 ? _direction.y * 0.1f : _direction.y;
        }
    }
}
