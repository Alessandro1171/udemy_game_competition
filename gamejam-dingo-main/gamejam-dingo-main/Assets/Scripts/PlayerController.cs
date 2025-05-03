using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 50;
    public int currentHealth;
    private Renderer playerRenderer;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> golems = new List<GameObject>();

    void Start()
    {
        currentHealth = maxHealth;
        playerRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamageAllEnemies();
            DamageAllGolems();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        playerRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        playerRenderer.material.color = Color.white;
    }

    void Die()
    {
        Debug.Log("Player has died.");
        Time.timeScale = 0f;
    }
  void DamageAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < 1.0f)
                {
                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(1);
                        Debug.Log("Enemy took 1 damage.");
                    }
                    else
                    {
                        Debug.LogError("Enemy script not found on an enemy GameObject.");
                    }
                }
            }
        }
    }

    void DamageAllGolems()
    {
        foreach (GameObject golem in golems)
        {
            if (golem != null)
            {
                float distanceToGolem = Vector3.Distance(transform.position, golem.transform.position);

                if (distanceToGolem < 1.0f)
                {
                    golem golemScript = golem.GetComponent<golem>();
                    if (golemScript != null)
                    {
                        golemScript.TakeDamage(1);
                        Debug.Log("Golem took 1 damage.");
                    }
                    else
                    {
                        Debug.LogError("Golem script not found on a golem GameObject.");
                    }
                }
            }
        }
    }
}
