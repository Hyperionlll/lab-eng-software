using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Chases player player when triggered
    private void FixedUpdate()
    {
        if (playerPos && isTrapTriggered)
            ChasePlayer();
    }
}
