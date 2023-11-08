using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrap : MonoBehaviour
{
    public GameObject[] ghosts;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            foreach (GameObject ghost in ghosts)
            {
                ghost.GetComponent<Ghost>().TrapTrigger();
            }
            triggered = false;
        }
    }
}
