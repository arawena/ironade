﻿using UnityEngine;
using System.Collections;

public class CreateBuildingScript : State {

    public GameObject building;
    private GameObject createdBuilding;

    void OnEnable () {
        createdBuilding = (GameObject)Instantiate(building, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }
	
	void Update () {
	    if(createdBuilding.GetComponent<BuildingScript>().currentState != createdBuilding.GetComponent<PositionScript>())
        {
            nextState = gameObject.GetComponent<SetupScene>();
        }
	}
}