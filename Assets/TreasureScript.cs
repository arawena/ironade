using UnityEngine;
using System.Collections;

public class TreasureScript : MonoBehaviour {

    public bool isTaken = false;

    private SpriteRenderer mRenderer;
	void Start () {
        mRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        mRenderer.enabled = !isTaken;
	}

    public void StateChange(bool taken, Vector3 worldCoordinates)
    {
        isTaken = taken;
        if(worldCoordinates != Vector3.zero)
        {
            MapGraph.sharedInstance.goal = MapGraph.sharedInstance.NodeFromWorldPoint(worldCoordinates);
            transform.position = MapGraph.sharedInstance.goal.centerWorldPos;
            PathFinding.sharedInstance.CalculatePathsForSpawnLocation();
        }
    }
}
