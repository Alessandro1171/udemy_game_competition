using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public AudioSource src2;
    public AudioClip hit,die;
    public float min = 3f;
    public float max = 4f;
    public Transform player;
    public float moveSpeed;
    public float rotationSpeed;
    public int damageAmount = 1;
    public float damageInterval = 5f;
    public int maxHealth = 5;
    private int currentHealth;
    private float damageTimer;
    public Slider healthSlider;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        moveSpeed = GenerateRandomSpeed();
        damageTimer = damageInterval;
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    private float GenerateRandomSpeed()
    {
        return Random.Range(min, max);
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned to the enemy.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < 1.0f)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                DamagePlayer();
                damageTimer = damageInterval;
            }
        }

        Vector3 direction = player.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        Vector3 direction2D = Quaternion.Euler(0, 0, 180) * direction;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction2D);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
    public void DamagePlayer()
    {
            TakeDamage(damageAmount);
            UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy has died.");
            src2.clip = die;
            src2.Play();
            gameObject.SetActive(false);
            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy has died.");
            gameObject.SetActive(false);
        }
        src2.clip = hit;
        src2.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(1);
            }
        }
    }
}