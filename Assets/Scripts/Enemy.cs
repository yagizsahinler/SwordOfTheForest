using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Düşmanın hareket hızı
    private Transform player; // Ana karakterin transformu
    private GameController gameController; // GameController referansı

    void Start()
    {
        // Ana karakteri ve GameController'ı bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ana karakterle çarpışma durumunda
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ana karakterin canını 2 azalt
            gameController.PlayerTakeDamage(2);

            // Düşmanı yok et
            Destroy(gameObject);
        }
    }
}
