using UnityEngine;
using System.Collections;

public class SetupScene : State
{
    public int availableBudget;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextState = gameObject.GetComponent<CreateBuildingScript>();
        } else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnWave spawnState = gameObject.GetComponent<SpawnWave>();
            nextState = spawnState;
        }
    }
}
