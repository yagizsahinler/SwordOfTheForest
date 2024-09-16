using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1f;
    public LayerMask enemyLayer;
    public int damage = 1;

    public Animator anim;
    public float attackCooldown = 1f; // Saldırı için bekleme süresi
    private bool canAttack = true; // Saldırı yapılıp yapılamayacağını kontrol eder

    public void Attack()
    {
        if (canAttack)
        {
            anim.SetBool("isAttacking", true);
            canAttack = false;
            StartCoroutine(CooldownCoroutine()); // Cooldown sürecini başlat
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
        }
    }

    // Cooldown için coroutine
    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown); // Belirlenen süre kadar bekle
        canAttack = true; // Saldırı tekrar yapılabilir hale gelsin
    }
}
