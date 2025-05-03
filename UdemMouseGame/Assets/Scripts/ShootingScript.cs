using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 1f;
    public float defaultRange = 6;

    public bool shotgunMode = false;
    public int shotgunPellets = 5;
    public float shotgunSpreadAngle = 15f;
    public float shotgunRange = 3.3f;

    public bool burstMode = false;
    public float burstDelay = 0.15f;
    public float burstRange = 5.5f;

    private float timeSinceLastShot = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shotgunMode = false;
            burstMode = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shotgunMode = true;
            burstMode = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotgunMode = false;
            burstMode = true;
        }
        if (Input.GetMouseButtonDown(0) && Time.time >= timeSinceLastShot + 1f / fireRate)
        {
            timeSinceLastShot = Time.time;
            if (shotgunMode)
            {
                ShootShotgun();
            } else if (burstMode)
            {
                StartCoroutine(ShootBurst());
            } else
            {
                Shoot();

            }
        }
    }

    void Shoot()
    {
        CreateBullet(firePoint.position, firePoint.rotation);
    }

    void ShootShotgun()
    {
        for (int i = 0; i < shotgunPellets; i++)
        {
            // Randomize the spread for each pellet
            float spread = Random.Range(-shotgunSpreadAngle, shotgunSpreadAngle);
            Quaternion pelletRotation = Quaternion.Euler(0, 0, spread) * firePoint.rotation;
            CreateBullet(firePoint.position, pelletRotation);
        }
    }
    IEnumerator ShootBurst()
    {
        for (int i = 0; i < 3; i++)
        {
            Shoot();
            yield return new WaitForSeconds(burstDelay);
        }
    }

    void CreateBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = rotation * Vector2.up; // Assuming the bullet prefab faces right
        rb.velocity = direction * bulletSpeed;

        RangeScript bulletRangeScript = bullet.GetComponent<RangeScript>();
        if (bulletRangeScript != null)
        {
            float range;
            if (shotgunMode)
            {
                range = shotgunRange;
            } else if (burstMode)
            {
                range = burstRange;
            } else
            {
                range = defaultRange;
            }

            bulletRangeScript.SetMaxDistance(range);
        }
    }
}
