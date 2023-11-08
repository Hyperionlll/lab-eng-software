using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HPPowerUp : MonoBehaviour
{
    private int healStrength = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health healthScript = collision.gameObject.GetComponent<Health>();
            if (healthScript.currentHealth < healthScript.maxHealth)
            {
                healthScript.Heal(healStrength);
                Destroy(gameObject);
            }
        }
    }
}
