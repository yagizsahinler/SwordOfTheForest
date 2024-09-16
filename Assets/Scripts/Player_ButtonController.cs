using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI butonları için gerekli

public class Player_ButtonController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private Animator _animator;
    private bool isGrounded;
    private SpriteRenderer _spriteRenderer;

    public Player_Combat player_Combat;

    // Butonlar için eklenen değişkenler
    private bool moveRight;
    private bool moveLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Hareket kontrolleri: Butonlara basılıp basılmadığını kontrol eder
        if (moveRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            _spriteRenderer.flipX = false;
        }
        else if (moveLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            _spriteRenderer.flipX = true;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Durduğunda
        }

        // Animasyonu güncelle
        _animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

    // Zıplama butonu için fonksiyon
    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Sağ ve sol butonlar için fonksiyonlar
    public void MoveRight(bool isPressed)
    {
        moveRight = isPressed;
    }

    public void MoveLeft(bool isPressed)
    {
        moveLeft = isPressed;
    }

    // Atak butonu için fonksiyon
    public void Attack()
    {
        player_Combat.Attack();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            _animator.SetBool("grounded", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            _animator.SetBool("grounded", false);
        }
    }
}
