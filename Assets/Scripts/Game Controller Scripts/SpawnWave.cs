using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CharacterType { Scout = 1, Tank };

public struct SpawnWaveIterator
{
    public int roundIndex;
    public int roundSpawnIndex;
}

public class SpawnWave : State
{
    public List<List<EnemySpawn>> spawnWave;

    public GameObject[] prefabs;

    private System.Random randomNumberGenerator = new System.Random();
    private SpawnWaveIterator sIterator;
    private int numberOfSpawnLocations;

    private EnemySpawn currentSpawn;
    private List<GameObject> activeEnemies;

    private float timeInRound;
    private bool doneSpawning;

    void OnEnable()
    {
        sIterator.roundIndex = 0;
        sIterator.roundSpawnIndex = 0;
        timeInRound = 0;
        doneSpawning = false;
        activeEnemies = new List<GameObject>();
        UpdateCurrentSpawn();
        numberOfSpawnLocations = GridOperations.sharedInstance.GetSpawnLocations().Count;
    }

    void UpdateCurrentSpawn()
    {
        currentSpawn = spawnWave[sIterator.roundIndex][sIterator.roundSpawnIndex];
    }

    void Update()
    {
        timeInRound += Time.deltaTime;
        if(!doneSpawning && timeInRound > currentSpawn.spawnTime)
        {
            int rand = randomNumberGenerator.Next(0, numberOfSpawnLocations);
            Node spawnLocation = MapGraph.sharedInstance.getSpawnNodes()[rand];
            GameObject relevantGO;
            GameObject newFarmer = (GameObject)Instantiate(prefabs[(int)currentSpawn.enemyType-1], spawnLocation.getOuterMostCoordinate(), Quaternion.identity);
            FarmerControllerScript farmerScript = newFarmer.GetComponent<FarmerControllerScript>();
            farmerScript.nextNode = spawnLocation;
            activeEnemies.Add(newFarmer);

            if(sIterator.roundSpawnIndex < spawnWave[sIterator.roundIndex].Count-1)
            {
                sIterator.roundSpawnIndex++;
                UpdateCurrentSpawn();
            } else if(sIterator.roundIndex < spawnWave.Count-1)
            {
                sIterator.roundIndex++;
                doneSpawning = true;
                timeInRound = 0;
                UpdateCurrentSpawn();
            } else
            {
                nextState = gameObject.GetComponent<SetupScene>();
                return;
            }
            
        } else if(doneSpawning)
        {
            int aliveCount = 0;
            for (var i = 0; i < activeEnemies.Count; i++)
            {
                if (activeEnemies[i] != null)
                {
                    aliveCount++;
                }
            }
            doneSpawning = aliveCount != 0 ? true : false;
            timeInRound = 0;
        }
    }
}
