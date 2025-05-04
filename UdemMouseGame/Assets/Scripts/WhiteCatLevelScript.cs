using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhiteCatLevelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySpawner1;

    [SerializeField]
    private GameObject enemySpawner2;

    [SerializeField]
    private GameObject enemySpawner3;

    [SerializeField]
    private GameObject enemySpawner4;
    
    public static WhiteCatLevelScript Instance;
    public List<GameObject> activeObjects = new();
    private int roundCounter = 0;
    private int enemyCounter = 0;
   
    public GameObject doorOpen;

    public GameObject doorClose;
    void Awake()
    {
        Instance = this;
        roundCounter = 0;
        doorOpen.SetActive(false);
        doorClose.SetActive(true);
    }
    public void LevelComplete()
    {
        enemyCounter--;
        if (enemyCounter==0 && roundCounter > 0)
        {
            doorOpen.SetActive(true);
            doorClose.SetActive(false);
        }
    }
    public void RegisterObject(GameObject obj)
    {
        Debug.Log("Game object:"+obj);
        obj.SetActive(true);
        activeObjects.Add(obj);
    }

    public void UnregisterObject(GameObject obj)
    {
        activeObjects.Remove(obj);
        Destroy(obj);
        Debug.Log("enemy count:" + activeObjects.Count);
        if (activeObjects.Count == 0)
        {
            //SpawnNextRound();
            Debug.Log("All enemies defeated!");
        }
    }
    public void SpawnNextRound()
    {
        if (roundCounter==0)
        {
            SpawnRound1();
        }
        else if (roundCounter == 1) {

            SpawnRound2();
        }
        else if (roundCounter == 2)
        {
            SpawnRound3();

        }
        else if (roundCounter == 3)
        {
            SpawnRound4();

        }
        else if (roundCounter == 4)
        {
            SpawnRound5();

        }
        else if (roundCounter == 5)
        {
            SpawnRound6();

        }
        else
        {
            doorOpen.SetActive(true);
            doorClose.SetActive(false);
        }
        roundCounter++;
    }
    public void SpawnRound1()
    {
        EnemyTypes[] round1 = new EnemyTypes[3] {EnemyTypes .red, EnemyTypes.red , EnemyTypes.red};
        enemyCounter = round1.Length * 4;
        Debug.Log("enemyCounter:"+ enemyCounter);
        enemySpawner1.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner2.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner3.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner4.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
    }

    public void SpawnRound2()
    {
        EnemyTypes[] round1 = new EnemyTypes[3] { EnemyTypes.green, EnemyTypes.green, EnemyTypes.green};
        enemySpawner1.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner2.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner3.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner4.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
    }


    public void SpawnRound3()
    {
        EnemyTypes[] round1 = new EnemyTypes[3] { EnemyTypes.blue, EnemyTypes.blue, EnemyTypes.blue};
        enemySpawner1.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner2.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner3.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
        enemySpawner4.GetComponent<EnemySpawnerScript>().SpawnCommands(round1);
    }


    public void SpawnRound4()
    {
        EnemyTypes[] topLeft = new EnemyTypes[4];
        EnemyTypes[] topRight = new EnemyTypes[4];
        EnemyTypes[] bottomLeft = new EnemyTypes[4];
        EnemyTypes[] bottomRight = new EnemyTypes[4];
        for (int i = 0; i < 4; i++)
        {
            topLeft[i] = AddRandomBlueGreen();
            topRight[i] = AddRandomBlueGreen();
            bottomLeft[i] = AddRandomBlueGreen();
            bottomRight[i] = AddRandomBlueGreen();
        }
        enemySpawner1.GetComponent<EnemySpawnerScript>().SpawnCommands(topLeft);
        enemySpawner2.GetComponent<EnemySpawnerScript>().SpawnCommands(bottomLeft);
        enemySpawner3.GetComponent<EnemySpawnerScript>().SpawnCommands(bottomRight);
        enemySpawner4.GetComponent<EnemySpawnerScript>().SpawnCommands(topRight);
    }

    public void SpawnRound5()
    {
        EnemyTypes[] topLeft = new EnemyTypes[4];
        EnemyTypes[] topRight = new EnemyTypes[4];
        EnemyTypes[] bottomLeft = new EnemyTypes[4];
        EnemyTypes[] bottomRight = new EnemyTypes[4];
        for (int i = 0; i < 4; i++)
        {
            topLeft[i] = AddRandomRedGreen();
            topRight[i] = AddRandomRedGreen();
            bottomLeft[i] = AddRandomRedGreen();
            bottomRight[i] = AddRandomRedGreen();
        }
        enemySpawner1.GetComponent<EnemySpawnerScript>().SpawnCommands(topLeft);
        enemySpawner2.GetComponent<EnemySpawnerScript>().SpawnCommands(bottomLeft);
        enemySpawner3.GetComponent<EnemySpawnerScript>().SpawnCommands(bottomRight);
        enemySpawner4.GetComponent<EnemySpawnerScript>().SpawnCommands(topRight);
    }
    public void SpawnRound6()
    {
        EnemyTypes[] topLeft = new EnemyTypes[4];
        EnemyTypes[] topRight = new EnemyTypes[4];
        EnemyTypes[] bottomLeft = new EnemyTypes[4];
        EnemyTypes[] bottomRight = new EnemyTypes[4];
        for (int i = 0; i < 4; i++)
        {
            topLeft[i] = AddRandomRedGreenBlue();
            topRight[i] = AddRandomRedGreenBlue();
            bottomLeft[i] = AddRandomRedGreenBlue();
            bottomRight[i] = AddRandomRedGreenBlue();
        }
        enemySpawner1.GetComponent<EnemySpawnerScript>().SpawnCommands(topLeft);
        enemySpawner2.GetComponent<EnemySpawnerScript>().SpawnCommands(bottomLeft);
        enemySpawner3.GetComponent<EnemySpawnerScript>().SpawnCommands(bottomRight);
        enemySpawner4.GetComponent<EnemySpawnerScript>().SpawnCommands(topRight);
    }
    public EnemyTypes AddRandomBlueGreen()
    {
        int randomInt = Random.Range(0, 2);
        if (randomInt == 0) {
            return EnemyTypes.blue;
        }
        else
        {
            return EnemyTypes.green;
        }
    }

    public EnemyTypes AddRandomRedGreen()
    {
        int randomInt = Random.Range(0, 2);
        if (randomInt == 0)
        {
            return EnemyTypes.red;
        }
        else
        {
            return EnemyTypes.green;
        }
    }

    public EnemyTypes AddRandomRedGreenBlue()
    {
        int randomInt = Random.Range(0, 3);
        if (randomInt == 0)
        {
            return EnemyTypes.red;
        }
        else if (randomInt == 1)
        {
            return EnemyTypes.green;
        }
        else
        {
            return EnemyTypes.blue;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
