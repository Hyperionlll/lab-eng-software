using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    protected bool isDead = false;
    public int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public Slider healthBar;

    private void Awake()
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
            GameObject parentObject = gameObject.transform.parent.gameObject;
            Destroy(parentObject);
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
