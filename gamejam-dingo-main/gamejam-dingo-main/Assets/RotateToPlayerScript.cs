using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayerScript : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();

            // Calculate the rotation required to look at the player
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 270); // Adjust as needed
        }
    }
}
