using UnityEngine;
using System.Collections;

public class SpawnWave : State
{

    public int numberOfEnemies;
    public GameObject farmer;
    bool doneSpawning = false;
    System.Random randomNumberGenerator = new System.Random();

    void OnEnable()
    {
        int numberOfSpawnLocations = GridOperations.sharedInstance.GetSpawnLocations().Count;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int rand = randomNumberGenerator.Next(0, numberOfSpawnLocations);
            Node spawnLocation = MapGraph.sharedInstance.getSpawnNodes()[rand];
            GameObject newFarmer = (GameObject)Instantiate(farmer, spawnLocation.getOuterMostCoordinate(), Quaternion.identity);
            FarmerControllerScript farmerScript = newFarmer.GetComponent<FarmerControllerScript>();
            farmerScript.nextNode = spawnLocation;
        }
        doneSpawning = true;
    }

    void Update()
    {
        if(doneSpawning)
        {
            nextState = gameObject.GetComponent<SetupScene>();
        }
    }
}
