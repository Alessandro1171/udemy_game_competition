using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRounds : MonoBehaviour
{
    private bool hasActivated = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasActivated) return;

        if (other.CompareTag("Player"))
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("LevelLogic");

            if (spawner != null)
            {
                WhiteCatLevelScript spawnerScript = spawner.GetComponent<WhiteCatLevelScript>();
                if (spawnerScript != null)
                {
                    spawnerScript.SpawnNextRound();
                    hasActivated = true;
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
}
