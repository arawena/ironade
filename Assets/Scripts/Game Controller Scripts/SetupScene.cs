using UnityEngine;
using System.Collections;

public class SetupScene : State
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextState = gameObject.GetComponent<CreateBuildingScript>();
        }
    }
}
