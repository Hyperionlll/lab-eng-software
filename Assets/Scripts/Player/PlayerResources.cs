using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class PlayerResources : Health
{
    [SerializeField] private float maxResource, resourceCooldown;

    private Color manaColor = new Color(0, 0, 1, 1);
    private Color staminaColor = new Color(0, 1, 0, 1);

    public float currentResource;
    public Slider resourceBar;

    private void Awake()
    {
        isDead = false;

        resourceBar.maxValue = maxResource;
        resourceBar.value = currentResource;
        //resourceBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = manaColor;
        resourceBar.fillRect.gameObject.SetActive(true);

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBar.fillRect.gameObject.SetActive(true);
    }

    public void SpendResource()
    {
        currentResource -= 1;
        resourceBar.value = currentResource;
        StartCoroutine(RecoverResource());
    }

    private IEnumerator RecoverResource()
    {
        yield return new WaitForSeconds(resourceCooldown);
        currentResource += 1;
        resourceBar.value = currentResource;
    }
}
