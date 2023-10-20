using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private bool isDead = false;

    public int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public Slider healthBar;

    public void Awake()
    {
        isDead = false;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBar.fillRect.gameObject.SetActive(true);
    }

    public void GetHit(int damage, GameObject sender)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth > 0)
            OnHitWithReference?.Invoke(sender);
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }

    public void Heal(int healStrength)
    {
        currentHealth += healStrength;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthBar.value = currentHealth;
    }
}
