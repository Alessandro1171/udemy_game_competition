using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeScript : MonoBehaviour
{
    public float maxDistance;

    private Vector2 startPosition;
    private float distanceTravelled;
    public int bulletDamage = 1;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }
    public void SetMaxDistance(float newMaxDistance)
    {
        maxDistance = newMaxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled = Vector2.Distance(startPosition, transform.position);

        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemyScript = collision.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(bulletDamage);
            }

            Destroy(gameObject); // Destroy the bullet
        }
    }
}
