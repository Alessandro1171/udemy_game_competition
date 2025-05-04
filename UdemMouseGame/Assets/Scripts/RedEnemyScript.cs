using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
public class RedEnemyScript : MonoBehaviour
{
   
    //public AudioSource src2;
    //public AudioClip hit, die;
    public float min = 3f;
    public float max = 4f;
    public Transform player;
    public float moveSpeed;
    public float rotationSpeed;
    public int damageAmount = 1;
    public float damageInterval = 5f;
    public int maxHealth = 3;
    private int currentHealth;
    private float damageTimer;
    public Slider healthSlider;
    private bool waitingToHeal;
    private float healTimer;
    public Rigidbody2D parentRigidbody2D;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        moveSpeed = GenerateRandomSpeed();
        damageTimer = damageInterval;
        currentHealth = maxHealth;
        waitingToHeal = false;
        healTimer = 60f;
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

        Vector2 direction = (player.position - transform.position).normalized;
        parentRigidbody2D.AddForce(direction * moveSpeed);

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
            //src2.clip = die;
            //src2.Play();
            gameObject.SetActive(false);

        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("taking:"+damage);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (!waitingToHeal)
        {
            waitingToHeal = true;
            //HealEnemy();
        }
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy has died.");
            WhiteCatLevelScript.Instance.LevelComplete();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        //src2.clip = hit;
        //src2.Play();
    }
    public void HealEnemy()
    {
        StartCoroutine(WaitThenHeal());
        currentHealth = maxHealth;
        waitingToHeal = false;

    }
    IEnumerator WaitThenHeal()
    {

        yield return new WaitForSeconds(60);

        Debug.Log("Waited " + healTimer + " seconds");
        // Continue execution here
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /* Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(1);
            }*/
        }
    }
}
