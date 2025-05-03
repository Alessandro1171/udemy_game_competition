using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject greenEnemyPrefab;

    [SerializeField]
    private GameObject redEnemyPrefab;

    [SerializeField]
    private GameObject blueEnemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    private float _timeUntilSpawn;


    void SpawnCommands(string[] enemyList)
    {
        foreach(string enemy in enemyList)
        {
            float spawnDelay = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
            StartCoroutine(WaitThenSpawn(spawnDelay));
            if (enemy == "red")
            {
                Instantiate(redEnemyPrefab, transform.position, Quaternion.identity);
            }
            else if(enemy == "green")
            {
                Instantiate(greenEnemyPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(blueEnemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }


    IEnumerator WaitThenSpawn(float seconds)
    {
        float elapsed = 0f;

        while (elapsed < seconds)
        {
            elapsed += Time.deltaTime;
            yield return null; // Wait for next frame
        }

        Debug.Log("Waited " + seconds + " seconds");
        // Continue execution here
    }

    
}
