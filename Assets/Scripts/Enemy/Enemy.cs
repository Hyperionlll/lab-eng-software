using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float aggroRange = 10f, patrolRange = 7.5f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float moveSpeed = 5f;

    private bool isAtBoundary = false;
    private bool patrolBackwards = false;
    private SpriteRenderer sprite;
    private Vector2 distance;
    private Vector2 initialPos;

    protected bool playerInRange = false;
    protected Rigidbody2D rb2d;
    protected Transform playerPos;
    protected Vector2 moveDirection;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        initialPos = transform.position;
    }

    private void Start()
    {
        playerPos = GameObject.Find("Player").transform;
    }

    protected virtual void Update()
    {
        if (!MainGameUIManager.isPaused)
            SearchPlayerPosition();
    }

    private void SearchPlayerPosition()
    {
        if (playerPos)
        {
            distance = playerPos.position - transform.position;
            moveDirection = distance.normalized;
            if (distance.x > -aggroRange && distance.x < aggroRange && !isAtBoundary)
                playerInRange = true;
            else
                playerInRange = false;
        }
    }

    protected void Patrol()
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
        TurnDirection();
    }

    protected virtual void ChasePlayer()
    {
        rb2d.velocity = new Vector2(moveDirection.x, 0) * moveSpeed;
        TurnDirection();
    }

    protected void TurnDirection()
    {
        if (rb2d.velocity.x < 0f)
        {
            sprite.flipX = true;
        }
        else if (rb2d.velocity.x > 0f)
        {
            sprite.flipX = false;
        }
        else if (rb2d.velocity.x == 0f)
        {
            if (distance.x < 0)
                sprite.flipX = true;
            else if (distance.x > 0)
                sprite.flipX = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBoundary"))
        {
            isAtBoundary = true;
            patrolBackwards = true;
            StartCoroutine(ResetBoundary());
        }
    }

    private IEnumerator ResetBoundary()
    {
        yield return new WaitForSeconds(1.5f);
        isAtBoundary = false;
    }
}
