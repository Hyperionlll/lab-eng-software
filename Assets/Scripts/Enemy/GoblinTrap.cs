using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTrap : MonoBehaviour
{
    [SerializeField] private float numberToSpawn, respawnDelay; // number of goblins to spawn
    private bool triggered = false; // checks if the trap was triggered - prevents errors
    private Vector3 spawnPos1 = new Vector3(212, 8.6f, 0);
    private Vector3 spawnPos2 = new Vector3(260, 3.89f, 0);

    public GameObject[] goblins; // goblins objects already in the scene to activate when triggered
    public GameObject goblinPrefab; // goblin prefab to spawn/instantiate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            foreach (GameObject goblin in goblins)
            {
                goblin.GetComponent<Goblin>().TrapTrigger();
            }
            StartCoroutine(SpawnGoblins());
        }
    }

    private IEnumerator SpawnGoblins()
    {
        yield return new WaitForSeconds(respawnDelay);
        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject goblin = (GameObject)Instantiate(goblinPrefab, spawnPos1, Quaternion.identity);
            goblin.GetComponentInChildren<Goblin>().TrapTrigger();
            spawnPos1.x += 0.5f;
            goblin = (GameObject)Instantiate(goblinPrefab, spawnPos2, Quaternion.identity);
            goblin.GetComponentInChildren<Goblin>().TrapTrigger();
            spawnPos2.x += 0.5f;
        }
    }
}
