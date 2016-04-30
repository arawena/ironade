using UnityEngine;
using System.Collections;

enum Direction{Up, Down, Left, Right};

public class FarmerControllerScript : MonoBehaviour {

	Animator motionAnimator;
	bool isFlipped = false;

	void Start () {
		motionAnimator = GetComponent<Animator> ();
	}


	void Update () {
		Direction mDirection = (Direction)motionAnimator.GetInteger ("direction");

		// XOR between the direction being right and the flipped property in order to apply/revert the rotation
		if (mDirection == Direction.Right != isFlipped) {
			Flip ();
			isFlipped = (mDirection==Direction.Right);
		}
	}

	void Flip () {
		Vector3 mScale = transform.localScale;
		mScale.x *= -1;
		transform.localScale = mScale;
	}
}
