using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 5;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;

    public float damageCooldown = 1f; // Hasar verme süresi (saniye)
    private float lastDamageTime = 0f; // Son hasar verilen zaman

    public float attackCooldown = 1.5f; // Saldırı arasındaki bekleme süresi
    private float lastAttackTime = 0f; // Son saldırı zamanı

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && Time.time >= lastDamageTime + damageCooldown)
        {
            playerHealth.ChangeHealth(-damage);
            Debug.Log("hasar alındı! Mevcut Can: " + playerHealth.currentHealth);

            lastDamageTime = Time.time;
            Debug.Log("Son hasar verilen zaman güncellendi: " + lastDamageTime);
        }
        else if (playerHealth == null)
        {
            Debug.LogWarning("Çarpışan nesnede PlayerHealth bileşeni yok!");
        }
        else
        {
            Debug.Log("Hasar verilemedi, cooldown süresi devam ediyor.");
        }
    }

    public void Attack()
    {
        // Saldırı arası cooldown süresi kontrolü
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

            if (hits.Length > 0)
            {
                hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
                Debug.Log("Attack fonksiyonu tetiklendi, hasar verildi.");

                // Son saldırı zamanını güncelle
                lastAttackTime = Time.time;
            }
        }
        else
        {
            Debug.Log("Saldırı bekleme süresi devam ediyor, saldırı yapılamadı.");
        }
    }
}
