using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public AudioSource src;
    public AudioClip shotgun,gun,die,dasher;

    public SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    public int numberOfFlashes = 3;

    public Transform spriteTransform;

    public int maxHealth = 5;
    public int currentHealth;

    //public HealthBar healthBar;

    public Animator anim;
    public float moveSpeed = 7;
    public Rigidbody2D rb;

    private bool canDash = true;
    private bool isDashing = false;
    public float dashingPower = 3f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 2f;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 19f;
    public float fireRate = 2f;
    public float defaultRange = 7;

    public bool shotgunMode = false;
    public int shotgunPellets = 5;
    public float shotgunSpreadAngle = 30f;
    public float shotgunRange = 3.5f;

    public bool burstMode = false;
    public float burstDelay = 0.05f;
    public float burstRange = 8f;

    private float timeSinceLastShot = 0f;

    private Vector2 moveDirection;

    [SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        RotateSpriteToMouse();
        if (isDashing)
        {
            return;
        }
        //Checks if the game has been lost
        LostGame(currentHealth);

        processInputs();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shotgunMode = false;
            burstMode = false;
            fireRate = 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shotgunMode = true;
            burstMode = false;
            fireRate = 0.7f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotgunMode = false;
            burstMode = true;
            fireRate = 2f;
        }
        if (Input.GetMouseButtonDown(0) && Time.time >= timeSinceLastShot + 1f / fireRate)
        {
            timeSinceLastShot = Time.time;
            if (shotgunMode)
            {
                ShootShotgun();
            }
            else if (burstMode)
            {
                StartCoroutine(ShootBurst());
            }
            else
            {
                Shoot();

            }
        }
    }

    void Shoot()
    {
        src.clip = gun;
        src.Play();
        CreateBullet(firePoint.position, firePoint.rotation);
    }

    void ShootShotgun()
    {
        src.clip = shotgun;
        src.Play();
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
            }
            else if (burstMode)
            {
                range = burstRange;
            }
            else
            {
                range = defaultRange;
            }

            bulletRangeScript.SetMaxDistance(range);
        }
    }

    void RotateSpriteToMouse()
    {
        // Convert the mouse position to world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure there's no z-axis modification

        // Calculate the direction from the sprite to the mouse
        Vector2 direction = (mouseWorldPosition - spriteTransform.position).normalized;

        // Calculate the angle to rotate
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the sprite
        spriteTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); //Quaternion.Euler(new Vector3(0f, 0f, angle - 90f)); // Adjust angle based on sprite orientation
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        move();
    }

    void processInputs()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        //--------Set Animation Bool---------
        //------------------------------------
        if (moveDirection.magnitude > 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        //-------------------------------------

        if (Input.GetAxisRaw("Dash") == 1 && canDash)
        {
            StartCoroutine(dash()); // Start the dash coroutine
        }
    }
    private void move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private IEnumerator dash()
    {
        src.clip = dasher;
        src.Play();

        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed * dashingPower, moveDirection.y * moveSpeed * dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        FlashRed();
        //healthBar.SetHealth(currentHealth);
    }
    public void HealDamage(int heal)
    {
        currentHealth += heal;
        //healthBar.SetHealth(currentHealth);
    }

    void LostGame(int health)
    {
        //src.clip = die;
        //src.Play();
        health = currentHealth;
        if(health == 0 || health < 0)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void FlashRed()
    {
        StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
