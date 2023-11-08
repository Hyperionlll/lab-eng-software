using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakingGround : MonoBehaviour
{
    [SerializeField] private float delayToBreak = 1f, delayToReactivate = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(BreakGround());
    }

    private IEnumerator BreakGround()
    {
        yield return new WaitForSeconds(delayToBreak);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(ReactivateGround());
    }

    private IEnumerator ReactivateGround()
    {
        yield return new WaitForSeconds(delayToReactivate);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
