using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainAnimations : PlayerAnimation
{
    bool _facingLeft = false;

   protected override void Update()
    {
        base.Update();
        FlipPlayer(); 
    }

    void FlipPlayer()
    {
        if (_playerMovement.Direction.x == 0 && _playerMovement.Direction.y == 0)
            return;

        if (_playerMovement.Direction.y != 0 && _playerMovement.Direction.x == 0)
        {
            _facingLeft = transform.localScale.x < 0 ? !_facingLeft : _facingLeft;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f);
        }
        else if (_playerMovement.Direction.x > 0 && _facingLeft || _playerMovement.Direction.x < 0 && !_facingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 1f);
            _facingLeft = _playerMovement.Direction.x < 0;
        }
    }
}
