using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HPPowerUp : MonoBehaviour
{
    [SerializeField] private int healStrength = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health healthScript = collision.gameObject.GetComponent<Health>();
            if (healthScript.currentHealth < healthScript.maxHealth)
            {
                AudioManager.instance.PlayHealSound();
                healthScript.Heal(healStrength);
                Destroy(gameObject);
            }
        }
    }
}
