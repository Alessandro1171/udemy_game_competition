using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;
    public Rigidbody2D rb;

    private bool canDash = true;
    private bool isDashing = false;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private Vector2 moveDirection;

    [SerializeField] private TrailRenderer tr;

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        processInputs();
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
}
