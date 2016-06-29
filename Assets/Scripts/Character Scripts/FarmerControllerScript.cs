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
    private Vector3 travelDistance;
    private float threshold;

    private bool forward;

	void Start () {
		motionAnimator = GetComponent<Animator> ();
        SpriteRenderer mRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = mRenderer.sprite.bounds.size;

        InitMotion();

        float smallerDimension = (GridOperations.sharedInstance.cellHeight > GridOperations.sharedInstance.cellWidth) ? GridOperations.sharedInstance.cellWidth: GridOperations.sharedInstance.cellHeight;

        pathIndex = 0;
        forward = true;

        path = PathFinding.sharedInstance.pathsForSpawnLocations[nextNode];

    }

    void InitMotion()
    {
        threshold = ((nextNode.centerWorldPos - transform.position) * 0.01F).magnitude;
        travelDistance = (nextNode.centerWorldPos - transform.position) * (GridOperations.sharedInstance.cellWidth * GetComponent<FarmerPropertiesScript>().speed);
        CalculateDirection();
    }


	void Update () {
        transform.position += travelDistance*Time.deltaTime;
        if (Vector3.Distance(transform.position, nextNode.centerWorldPos) < threshold)
        {
            transform.position = nextNode.centerWorldPos;
            nextNode = path[pathIndex];
            if (forward && pathIndex<path.Count-1)
            {
                pathIndex++;
            } else if(!forward)
            {
                pathIndex--;
            } else
            {
                forward = false;
                pathIndex--;
            }
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

    public void GetHit (float health)
    {
        GetComponent<FarmerPropertiesScript>().health -= health;
        if (GetComponent<FarmerPropertiesScript>().health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
