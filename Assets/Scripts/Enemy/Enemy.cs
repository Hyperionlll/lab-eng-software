using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float aggroRange = 10f, patrolRange = 7.5f;
    [SerializeField] private bool patrolBackwards = false;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float moveSpeed = 5f;

    private bool isAtBoundary = false;
    private Vector2 initialPos;

    [SerializeField] protected bool isGrounded = true;
    protected bool isTrapTriggered = false;
    protected bool playerInRange = false;
    protected Rigidbody2D rb2d;
    protected SpriteRenderer sprite;
    protected Transform playerPos;
    protected Vector2 distance;
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
        else if (transform.position.x < (initialPos.x + patrolRange) && patrolBackwards)
        {
            rb2d.velocity = new Vector2(1, 0) * moveSpeed;
        }
        else
        {
            patrolBackwards = !patrolBackwards;
        }
        TurnDirection();
    }

    protected virtual void ChasePlayer()
    {
        if (isGrounded)
            rb2d.velocity = new Vector2(moveDirection.x, 0) * moveSpeed;
        TurnDirection();
    }

    protected virtual void TurnDirection()
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBoundary"))
        {
            isAtBoundary = true;
            patrolBackwards = !patrolBackwards;
            StartCoroutine(ResetBoundary());
        }
    }

    private IEnumerator ResetBoundary()
    {
        yield return new WaitForSeconds(1.5f);
        isAtBoundary = false;
    }

    public void TrapTrigger()
    {
        isTrapTriggered = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
