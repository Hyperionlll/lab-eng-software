using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float aggroRange = 10f;

    private float moveSpeed = 5f;
    private Rigidbody2D rb2d;
    private Transform target; // target == player
    private Vector2 moveDirection;
    private Vector2 direction;
    private Vector2 initialPos;
    private float patrolRange = 7.5f;
    private bool patrolBackwards = false;
    private bool targetInRange = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.isPaused)
            SearchTargetPosition();
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void SearchTargetPosition()
    {
        if (target)
        {
            direction = target.position - transform.position;
            moveDirection = direction.normalized;
            if (direction.x > -aggroRange && direction.x < aggroRange)
                targetInRange = true;
            else
                targetInRange = false;
        }
    }

    private void Movement()
    {
        // Se encontrar o player e ele estiver dentro do range definido, o inimigo deve seguir o player
        if (target && targetInRange)
        {
            rb2d.velocity = new Vector2(moveDirection.x, 0) * moveSpeed;
            RotateOnMovement();
        }
        // Caso contrario, o inimigo deve fazer o movimento de patrulha
        else
        {
            if (transform.position.x > (initialPos.x - patrolRange) && !patrolBackwards)
            {
                rb2d.velocity = new Vector2(-1, 0) * moveSpeed;
            }
            else if (transform.position.x < initialPos.x)
            {
                rb2d.velocity = new Vector2(1, 0) * moveSpeed;
                patrolBackwards = true;
            }
            else
            {
                patrolBackwards = false;
                initialPos = transform.position;
            }
            RotateOnMovement();
        }
    }

    private void RotateOnMovement()
    {
        if (rb2d.velocity.x <= 0 && transform.rotation.eulerAngles.y != 180)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (rb2d.velocity.x > 0 && transform.rotation.eulerAngles.y > 0)
        {
            transform.Rotate(0, -180, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.GetHit(damage, gameObject);
        }
    }
}
