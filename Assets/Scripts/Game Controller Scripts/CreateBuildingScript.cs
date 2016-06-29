using UnityEngine;
using System.Collections;

public class CreateBuildingScript : State {

    public GameObject building;
    private GameObject createdBuilding;

    void OnEnable () {
        createdBuilding = (GameObject)Instantiate(building, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }
	
	void Update () {
        State a = createdBuilding.GetComponent<BuildingScript>().currentState;

        if (createdBuilding.GetComponent<BuildingScript>().currentState && createdBuilding.GetComponent<BuildingScript>().currentState != createdBuilding.GetComponent<PositionScript>())
        {
            nextState = gameObject.GetComponent<SetupScene>();
        }
	}
}
