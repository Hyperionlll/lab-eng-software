using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Chases player if player is in range
    // Patrols if player is out range
    private void FixedUpdate()
    {
        if (playerPos && playerInRange)
            ChasePlayer();
        else
            Patrol();
    }
}
