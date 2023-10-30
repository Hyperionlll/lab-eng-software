using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    private float strength = 20f;
    private float delay = 0.25f;

    public UnityEvent OnBegin, OnDone;

    public void PlayKnockback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        rb2d.velocity = Vector3.zero;
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
        if (gameObject.CompareTag("Player"))
            rb2d.AddForce(Vector2.up * (strength/2), ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
