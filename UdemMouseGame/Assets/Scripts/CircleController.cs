using System.Collections;
using UnityEngine;
public class golem : MonoBehaviour
{
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
    private int currentHealth;
    private void Start()
    {
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
        if (player == null)
        {
            Debug.LogWarning("Player not assigned to the golem.");
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
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
    }
    
  public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Golem took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Golem has died.");
        gameObject.SetActive(false);
    }
}
