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
            AudioManager.instance.PlayHealSound();
            Health healthScript = collision.gameObject.GetComponent<Health>();
            if (healthScript.currentHealth < healthScript.maxHealth)
            {
                healthScript.Heal(healStrength);
                Destroy(gameObject);
            }
        }
    }
}
