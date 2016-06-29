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

    public GameObject scout;
    public GameObject tank;

    private System.Random randomNumberGenerator = new System.Random();
    private SpawnWaveIterator sIterator;
    private int numberOfSpawnLocations;

    private EnemySpawn currentSpawn;

    private float timeInRound;

    void OnEnable()
    {
        sIterator.roundIndex = 0;
        sIterator.roundSpawnIndex = 0;
        timeInRound = 0;
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
        if(timeInRound > currentSpawn.spawnTime)
        {
            int rand = randomNumberGenerator.Next(0, numberOfSpawnLocations);
            Node spawnLocation = MapGraph.sharedInstance.getSpawnNodes()[rand];

            GameObject newFarmer = (GameObject)Instantiate(currentSpawn.enemyType==CharacterType.Scout?scout:tank, spawnLocation.getOuterMostCoordinate(), Quaternion.identity);
            FarmerControllerScript farmerScript = newFarmer.GetComponent<FarmerControllerScript>();
            farmerScript.nextNode = spawnLocation;

            if(sIterator.roundSpawnIndex < spawnWave[sIterator.roundIndex].Count-1)
            {
                sIterator.roundSpawnIndex++;
            } else if(sIterator.roundIndex < spawnWave.Count-1)
            {
                sIterator.roundIndex++;
                timeInRound = 0;
            } else
            {
                nextState = gameObject.GetComponent<SetupScene>();
                return;
            }
            UpdateCurrentSpawn();
        }
    }
}
