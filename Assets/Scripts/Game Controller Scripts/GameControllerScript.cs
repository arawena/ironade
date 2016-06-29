using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct EnemySpawn
{
    public CharacterType enemyType;

    public float spawnTime;
}

public struct LevelDescription
{
    public int budget;
    public List<List<EnemySpawn>> enemySpawnDetails;
}

public class GameControllerScript : StateMachine {

    public List<LevelDescription> spawnList;

    int level;
    private int overallScore = 0;

	void Start () {
        spawnList = FileOperations.getLevelDescription("spawnPresets");
        InitializeLevel(0, 0);
    }

    public void InitializeLevel(int lvl, int score)
    {
        level = lvl;
        overallScore += score;
        GetComponent<SetupScene>().availableBudget = spawnList[level].budget;
        currentState = gameObject.GetComponent<SetupScene>();
        SpawnWave spawnWaves = GetComponent<SpawnWave>();
        spawnWaves.spawnWave = spawnList[level].enemySpawnDetails;
    }

}
