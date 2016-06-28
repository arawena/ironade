using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum Direction{None, Up, Down, Left, Right};

public class FarmerControllerScript : MonoBehaviour {

	private Animator motionAnimator;
	private bool isFlipped = false;
    public Node nextNode;

    private List<Node> path;

    private int pathIndex;

    private const float fracJourney = 0.01F;
    private Vector3 travelDistance;

	void Start () {
		motionAnimator = GetComponent<Animator> ();
        InitMotion();

        float smallerDimension = (GridOperations.sharedInstance.cellHeight > GridOperations.sharedInstance.cellWidth) ? GridOperations.sharedInstance.cellWidth: GridOperations.sharedInstance.cellHeight;

        pathIndex = 0;

        path = PathFinding.sharedInstance.pathsForSpawnLocations[nextNode];

    }

    void InitMotion()
    {
        travelDistance = (nextNode.centerWorldPos - transform.position) * 0.01f;
        CalculateDirection();
    }


	void Update () {
        transform.position += travelDistance;
        if (Vector3.Distance(transform.position, nextNode.centerWorldPos) < travelDistance.magnitude && pathIndex != path.Count-1)
        {
            transform.position = nextNode.centerWorldPos;
            nextNode = path[pathIndex];
            pathIndex++;
            InitMotion();
        }
	}

	void Flip () {
		Vector3 mScale = transform.localScale;
		mScale.x *= -1;
		transform.localScale = mScale;
	}

    void CalculateDirection()
    {
        Direction currentDirection;
        Direction pastDirection = (Direction)motionAnimator.GetInteger("direction");
        if (transform.position.x > nextNode.centerWorldPos.x)
        {
            currentDirection = Direction.Left;
        } else if (transform.position.x < nextNode.centerWorldPos.x)
        {
            currentDirection = Direction.Right;
        } else if (transform.position.y > nextNode.centerWorldPos.y)
        {
            currentDirection = Direction.Up;
        } else
        {
            currentDirection = Direction.Down;
        }

        motionAnimator.SetInteger("direction", (int)currentDirection);

        if ((currentDirection == Direction.Right) != isFlipped)
        {
            Flip();
            isFlipped = (currentDirection == Direction.Right);
        }
        
    }
}
