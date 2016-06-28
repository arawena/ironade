using UnityEngine;
using System.Collections;

public class SetupScene : State
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextState = gameObject.GetComponent<CreateBuildingScript>();
        } else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnWave spawnState = gameObject.GetComponent<SpawnWave>();
            spawnState.numberOfEnemies = 1;
            nextState = spawnState;
        }
    }
}
