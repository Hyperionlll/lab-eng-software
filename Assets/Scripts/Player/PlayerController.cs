using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fireCooldownTimer = 0.5f;
    [SerializeField] private float jumpForce = 35f;
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private bool isGrounded = true, fireCooldown = false;
    private Rigidbody2D playerRb;

    public GameObject projectile;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!MainGameUIManager.isPaused)
        {
            Movement();
            FireProjectile();
        }
    }

    // Verifica se o player esta no chao ou no ar
    // Usado para impedir que o player pule varias vezes no ar
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Se estiver tocando no chao (objeto com a tag Ground)
            isGrounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Se estiver tocando no chao (objeto com a tag Ground)
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Se estiver tocando no chao (objeto com a tag Ground)
            isGrounded = false;
    }

    // Input de movimento
    private void Movement()
    {
        // Input de mover (horizontal - direita, esquerda)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Rotacao do player de acordo com o lado para o qual se movimenta (direita ou esquerda)
        if (horizontalInput < 0 && transform.rotation.eulerAngles.y != 180)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (horizontalInput > 0 && transform.rotation.eulerAngles.y == 180)
        {
            transform.Rotate(0, -180, 0);
        }
        // Movimentacao
        playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);

        // Input de pular
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(new Vector2(playerRb.velocity.x, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    // Input de atirar
    private void FireProjectile()
    {
        if (Input.GetButtonDown("Fire1") && !fireCooldown)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            fireCooldown = true;
            StartCoroutine(FireCooldown());
        }
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(fireCooldownTimer);
        fireCooldown = false;
    }
}
