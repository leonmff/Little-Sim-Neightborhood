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

    PlayerInputController _playerInput;

    Rigidbody2D _rb;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInputController>();

        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (_playerInput.GetInputHorizontalVertical(ref _direction))
        {
            // Moving
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * _speedMovement;
    }
}
