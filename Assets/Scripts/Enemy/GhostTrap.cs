using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrap : MonoBehaviour
{
    private bool triggered = false; // checks if the trap was triggered - prevents errors

    public GameObject[] ghosts; // ghosts objects already in the scene to activate when triggered

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            foreach (GameObject ghost in ghosts)
            {
                ghost.GetComponent<Ghost>().TrapTrigger();
            }
        }
    }
}
