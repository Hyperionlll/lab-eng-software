using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Bat : Enemy
{
    // No need to SearchPlayerPosition()
    protected override void Update()
    {
        
    }

    // Always patrols - never chases player
    void FixedUpdate()
    {
        Patrol();
    }

    protected override void TurnDirection()
    {
        if (rb2d.velocity.x < 0f)
        {
            sprite.flipX = false;
        }
        else if (rb2d.velocity.x > 0f)
        {
            sprite.flipX = true;
        }
        else if (rb2d.velocity.x == 0f)
        {
            if (distance.x < 0)
                sprite.flipX = false;
            else if (distance.x > 0)
                sprite.flipX = true;
        }
    }
}
