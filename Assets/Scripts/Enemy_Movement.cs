using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    public float chaseDistance = 5f; // Oyuncuya olan takip mesafesi
    public float attackRange = 2f; // Saldırı mesafesi

    private Rigidbody2D rb;
    public Transform player;

    private Animator anim;
    private EnemyState enemyState;
    private Enemy_Combat enemyCombat; // Enemy_Combat referansı ekleniyor

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyCombat = GetComponent<Enemy_Combat>(); // Enemy_Combat bileşenini al

        // İlk durum 'Idle' olarak ayarlanıyor
        ChangeState(EnemyState.Idle);

        if (player == null) // Eğer player atanmadıysa
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // "Player" tag'ine sahip objeyi bul
            if (playerObject != null)
            {
                player = playerObject.transform; // Transform referansını ata
            }
            else
            {
                Debug.LogError("'Player' etiketine sahip oyuncu nesnesi sahnede bulunamadı.");
            }
        }
    }

    void Update()
    {
        // Oyuncuya olan mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        // Mesafeye göre durum değişikliği
        if (distanceToPlayer <= attackRange)
        {
            if (enemyState != EnemyState.Attacking) // Eğer şu an 'Attacking' durumda değilse
            {
                ChangeState(EnemyState.Attacking);
            }
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            if (enemyState != EnemyState.Chasing) // Eğer şu an 'Chasing' durumda değilse
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            if (enemyState != EnemyState.Idle) // Eğer şu an 'Idle' durumda değilse
            {
                ChangeState(EnemyState.Idle);
            }
        }

        // Düşman 'Chasing' durumunda ise hareket etsin
        if (enemyState == EnemyState.Chasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else if (enemyState == EnemyState.Idle || enemyState == EnemyState.Attacking)
        {
            // Düşman 'Idle' veya 'Attacking' durumunda ise hareket etmesin
            rb.velocity = Vector2.zero;
        }

        // Attacking durumunda Attack fonksiyonunu çağır
        if (enemyState == EnemyState.Attacking)
        {
            enemyCombat.Attack();
        }

        FlipTowardsPlayer();
    }

    void FlipTowardsPlayer()
    {
        // Oyuncu düşmanın sağında mı solunda mı kontrol et
        if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Düşmanın yüz yönünü değiştir
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // x ekseninde ölçeği ters çevir
        transform.localScale = scale;
    }

    void ChangeState(EnemyState newState)
    {
        //şu anki animasyondan çıkma
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        //state değiştirme
        enemyState = newState;

        //yeni animasyona geçme
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
            enemyCombat.Attack(); // Attacking durumuna geçildiğinde Attack fonksiyonunu çağır
        }
    }

    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }
}
