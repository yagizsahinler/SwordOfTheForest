using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator _animator;
    private bool isGrounded;
    private SpriteRenderer _spriteRenderer;
    private bool isAttack1 = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        _animator.SetFloat("speed", Mathf.Abs(moveInput));

        //flip the sprite depending on the main characters movement way
        if (moveInput > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //check the mouse clicks for transition between attack animations
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked!");
            if (isAttack1)
            {
                _animator.SetTrigger("attack1");
                Debug.Log("Attack1 Triggered");
            }
            else
            {
                _animator.SetTrigger("attack2");
                Debug.Log("Attack2 Triggered");
            }
            isAttack1 = !isAttack1;
        }
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

