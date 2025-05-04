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


    public void SpawnCommands(EnemyTypes[] enemyList)
    {
        foreach(EnemyTypes enemy in enemyList)
        {
            float spawnDelay = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
            StartCoroutine(WaitThenSpawn(spawnDelay, enemy));
            
        }
    }


    IEnumerator WaitThenSpawn(float seconds, EnemyTypes enemy)
    {

        
        yield return new WaitForSeconds(seconds);
        if (enemy == EnemyTypes.red)
        {
            WhiteCatLevelScript.Instance.RegisterObject(Instantiate(redEnemyPrefab, transform.position, Quaternion.identity));
        }
        else if (enemy == EnemyTypes.green)
        {
            WhiteCatLevelScript.Instance.RegisterObject(Instantiate(greenEnemyPrefab, transform.position, Quaternion.identity));
        }
        else
        {
            WhiteCatLevelScript.Instance.RegisterObject(Instantiate(blueEnemyPrefab, transform.position, Quaternion.identity));
        }
        Debug.Log("Waited " + seconds + " seconds");
        // Continue execution here
    }

    
}
