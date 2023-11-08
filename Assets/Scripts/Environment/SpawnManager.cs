using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //private float startDelay = 1.5f;
    //private float repeatRate = 5f;
    private Vector3 spawnPosition = new(25f, -2.379688f, 0f);

    public GameObject enemyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(spawnEnemy), startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnEnemy()
    {
        Instantiate(enemyToSpawn, spawnPosition, transform.rotation);
        //spawnPosition.x *= -1;
        //Instantiate(enemyToSpawn, spawnPosition, transform.rotation);
        //spawnPosition.x *= -1;
    }
}
