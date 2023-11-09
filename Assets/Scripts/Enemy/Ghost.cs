using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ghost : Enemy
{
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

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.GetHit(damage, gameObject);
        }
    }
}
