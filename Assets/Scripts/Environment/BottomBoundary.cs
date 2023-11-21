using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottomBoundary : MonoBehaviour
{
    public UnityEvent callDeathScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            callDeathScreen.Invoke();
            Health health = collision.gameObject.GetComponent<Health>();
            health.GetHit(99, gameObject);
        }
        else
            Destroy(collision.gameObject);
    }
}
