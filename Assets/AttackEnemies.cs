using UnityEngine;
using System.Collections;

public class AttackEnemies : State
{

    GameObject enemy;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (enemy)
        {

        }
        else
        {
            nextState = GetComponent<LookForEnemiesScript>();
        }
    }
}
