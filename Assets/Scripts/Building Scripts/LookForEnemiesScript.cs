using UnityEngine;
using System.Collections;

public class LookForEnemiesScript : State {

    private Node currentNode;
	void Start () {
        currentNode = MapGraph.sharedInstance.NodeFromWorldPoint(transform.position);
	}
	
	void Update () {
	
	}
}
