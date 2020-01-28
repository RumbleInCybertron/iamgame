using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public float attackRangeKatana = 0.7f;
    public int attackDamage = 40;
    public int katanaDamageAddition = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                if(GetComponent<Player>().hasKatana) 
                { 
                    katanaAttack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    return;
                }
                fistAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void fistAttack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);   
        // Creates a circle from the attackPoint with a radius we specify
        // and collects all objects that it hits third parameter 
        // filters out enemies
        
        // Damage enemy in range
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void katanaAttack()
    {
        // Play an attack animation
        animator.SetTrigger("KatanaSlash");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Creates a circle from the attackPoint with a radius we specify
        // and collects all objects that it hits third parameter 
        // filters out enemies

        // Damage enemy in range
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage + katanaDamageAddition);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
