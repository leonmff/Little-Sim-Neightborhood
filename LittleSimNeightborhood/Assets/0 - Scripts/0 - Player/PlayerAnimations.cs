using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    bool _facingLeft = false;

    Animator _anim;

    PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PlayMovementAnimation();
        FlipPlayer();
    }

    void PlayMovementAnimation()
    {
        if (_playerMovement.Direction != Vector2.zero)
        {
            _anim.SetFloat("Horizontal", _playerMovement.Direction.x);
            _anim.SetFloat("Vertical", _playerMovement.Direction.y);
        }
    }

    void FlipPlayer()
    {
        float t_directionX = _playerMovement.Direction.x;
        if (t_directionX == 0)
            return;

        if (t_directionX > 0 && _facingLeft || t_directionX < 0 && !_facingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 1f);
            _facingLeft = t_directionX < 0;
        }
    }
}
