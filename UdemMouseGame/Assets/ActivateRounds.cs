using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRounds : MonoBehaviour
{
    private bool hasActivated = false;

    private bool playerInRange = false;
    void Update()
    {
        if (hasActivated) return;

        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("LevelLogic");

            if (spawner != null)
            {
                WhiteCatLevelScript spawnerScript = spawner.GetComponent<WhiteCatLevelScript>();
                if (spawnerScript != null)
                {
                    spawnerScript.SpawnNextRound();
                    hasActivated = true;
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("No MonsterSpawner script found on the spawner.");
                }
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
