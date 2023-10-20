using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float position = 0f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private Vector2 initialPos;
    [SerializeField] private float maxRange = 10f;
    [SerializeField] private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Posicao inicial do projetil quando instanciado
        transform.Translate(initialPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.isPaused)
            MoveForward();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.GetHit(damage, gameObject);
            Destroy(gameObject);
        }
    }

    // Movimento do projetil
    private void MoveForward()
    {
        Vector2 move = new(speed * Time.deltaTime, 0);
        transform.Translate(move);
        position += move.x;

        // Range maximo do projetil -> sera destruido ao passar do max range
        if (position > maxRange)
        {
            Destroy(gameObject);
        }
    }
}
