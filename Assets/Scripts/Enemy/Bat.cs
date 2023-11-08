using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
