using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    private bool hasActivated = false;

    private bool playerInRange = false;

    [SerializeField]
    private GameObject gameOverUI;
    void Update()
    {
        if (hasActivated) return;

        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            

            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogWarning("No GameObject found with tag 'monster_spawner'.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
