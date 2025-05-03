using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 50;
    public Renderer playerRenderer;
    private int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

        yield return new WaitForSeconds(0.2f);

        playerRenderer.material.color = Color.white;
    }

    public void Die()
    {
        Debug.Log("Player has died.");
        Time.timeScale = 0f;
    }
}
