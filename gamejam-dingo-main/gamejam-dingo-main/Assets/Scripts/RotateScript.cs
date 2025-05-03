using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public Transform spriteTransform; // Assign the sprite's transform here in the inspector

    void Update()
    {
        RotateSpriteToMouse();
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
        spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f)); // Adjust angle based on sprite orientation
    }
}
