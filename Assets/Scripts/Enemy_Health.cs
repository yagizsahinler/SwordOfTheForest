using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public int scoreValue = 10; // Düşman öldüğünde kazanılacak puan

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            Die(); // Ölüm fonksiyonunu çağır
        }
    }

    void Die()
    {
        // GameManager'a puan ekle
        Game_Manager.instance.AddScore(scoreValue);
        Destroy(gameObject); // Düşmanı yok et
    }
}
