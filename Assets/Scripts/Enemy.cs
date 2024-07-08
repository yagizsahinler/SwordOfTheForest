using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    
    public float speed = 2f; // Düşmanın hareket hızı
    private Transform player; // Ana karakterin transformu
    private GameController gameController; // GameController referansı

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        // Ana karakteri ve GameController'ı bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        // Ana karaktere doğru hareket et
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("hurt");

        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy died");

        animator.SetBool("isDead", true);

        //disable the enemy
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
