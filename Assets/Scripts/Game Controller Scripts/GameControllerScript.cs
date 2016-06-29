using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

	void Start () {
        currentState = GetComponent<LevelScreenScript>();
        currentState.enabled = true;
    }

}
