using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool angry;
    public float min = 1f;
    public float max = 2f;
    public Transform player;
    public float moveSpeed;
    public int regularDamage = 1;
    public int attackDamage = 5;
    public float damageInterval = 1f;
    public float attackInterval = 5f;
    private float damageTimer;
    private float attackTimer;
    public int maxHealth = 10;
    public bool isPhase2;
    private int currentHealth;
    public float rotationSpeed = -30f; 
    private void Start()
    {
        isPhase2 = false;
        moveSpeed = GenerateRandomSpeed();
        damageTimer = damageInterval;
        attackTimer = attackInterval;
        currentHealth = maxHealth;
    }
    private float GenerateRandomSpeed()
    {
        return Random.Range(min, max);
    }

    private void Update()
    {
        // fix rotation script maybe
        //transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        if (player == null)
        {
            Debug.LogWarning("Player not assigned to the boss.");
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // make this correspond to projectiles
        if (distanceToPlayer < 1.5f)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                TakeDamage(regularDamage);
                damageTimer = damageInterval;
            }
        }
        if (distanceToPlayer < 0.5f)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                TakeDamage(attackDamage);
                attackTimer = attackInterval;
            }
        }
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if(isPhase2){
            moveSpeed = 4f;
        }
    }
    
  public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0 && !isPhase2){
            Debug.Log("Entering phase 2");
            isPhase2 = true;
            currentHealth = maxHealth;
        }else if(currentHealth <= 0){
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss has died.");
        gameObject.SetActive(false);
    }
}
