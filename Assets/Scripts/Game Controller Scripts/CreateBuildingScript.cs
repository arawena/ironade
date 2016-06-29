using UnityEngine;
using System.Collections;

public enum BuildingType {Sun, Moon};

public class CreateBuildingScript : State {

    public GameObject[] buildings;
    private GameObject createdBuilding;

    public BuildingType type;

    void OnEnable () {
        createdBuilding = (GameObject)Instantiate(buildings[(int)type], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }
	
	void Update () {
        State a = createdBuilding.GetComponent<BuildingScript>().currentState;

        if (createdBuilding.GetComponent<BuildingScript>().currentState && createdBuilding.GetComponent<BuildingScript>().currentState != createdBuilding.GetComponent<PositionScript>())
        {
            gameObject.GetComponent<SetupScene>().availableBudget -= createdBuilding.GetComponent<BuildingProperties>().price; 
            nextState = gameObject.GetComponent<SetupScene>();
        }
	}
}
