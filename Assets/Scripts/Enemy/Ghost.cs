using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ghost : Enemy
{
    private bool isTrapTriggered = false;

    // Always chases player
    void FixedUpdate()
    {
        if (playerPos && isTrapTriggered)
            ChasePlayer();
        TurnDirection();
    }

    protected override void ChasePlayer()
    {
        rb2d.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.GetHit(damage, gameObject);
        }
    }

    public void TrapTrigger()
    {
        isTrapTriggered = true;
    }
}
