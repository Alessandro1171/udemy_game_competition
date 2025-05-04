using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Color toggleColor = Color.red;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private Coroutine colorCoroutine;
    private bool isTouchingZone = false;

    private bool active;
    private bool triggered;
    private Collider2D collision;


    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.green;
    }


    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("ColorZone"))
        {
            Debug.Log("Exited ColorZone!");
            isTouchingZone = false;
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
            spriteRenderer.color = originalColor;
        }
    }

    IEnumerator ToggleColor()
    {
        while (isTouchingZone)
        {
            spriteRenderer.color = (spriteRenderer.color == originalColor) ? toggleColor : originalColor;
            Debug.Log("Toggling color to: " + spriteRenderer.color);
            yield return new WaitForSeconds(0.5f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (collision.tag == "Player")
            Debug.Log("Trigger entered with: " + other.name);

        if (other.CompareTag("ColorZone"))
        {
            if (!triggered)
                isTouchingZone = true;

            if (colorCoroutine == null)
            {
                Debug.Log("colorCoroutine is null" );
            }
            if (active)
            {
                // Remove points
                colorCoroutine = StartCoroutine(ToggleColor());
            }
        }
    }
}
