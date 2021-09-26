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
