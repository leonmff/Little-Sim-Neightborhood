using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    protected PlayerMovement _playerMovement;
    protected Animator _anim;

    protected virtual void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        if (!_playerMovement)
            _playerMovement = transform.root.GetComponent<PlayerMovement>();

        _anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update() => PlayMovementAnimation();

    protected void PlayMovementAnimation()
    {
        if (!_anim || !_anim.runtimeAnimatorController)
            return;

        if (_playerMovement.Direction != Vector2.zero)
            SetAnimationParameters();
    }

    void SetAnimationParameters()
    {
        _anim.SetFloat("Horizontal", _playerMovement.Direction.x);
        _anim.SetFloat("Vertical", _playerMovement.Direction.y);
    }

    protected void SetAnimationParameters(Vector2 pDirection)
    {
        _anim.SetFloat("Horizontal", pDirection.x);
        _anim.SetFloat("Vertical", pDirection.y);
    }
}
