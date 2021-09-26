using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float _speedMovement = 0f;
    [SerializeField, InspectorReadOnly]
    Vector2 _direction;
    public Vector2 Direction { get => _direction; }

    Vector2 _directionLast;
    public Vector2 DirectionLast { get => _directionLast; }

    PlayerInputController _playerInput;
    Rigidbody2D _rb;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInputController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() => Movement();

    void Movement()
    {
        Vector2 t_directionLast = _direction;

        if (_playerInput.GetInputHorizontalVertical(ref _direction))
            _directionLast = t_directionLast;
    }

    private void FixedUpdate() => _rb.velocity = _direction * _speedMovement;
}
