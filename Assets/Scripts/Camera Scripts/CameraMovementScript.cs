using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {
	public float cameraSpeed = 3f;

	private float[] cameraPositionBounds;

	void Start () {
		float cameraPlaneHeight = gameObject.GetComponent<Camera>().orthographicSize;  
		float cameraPlaneWidth = cameraPlaneHeight * Screen.width / Screen.height;

		cameraPositionBounds = new float[4];

		cameraPositionBounds [Coordinate.Top] = GridOperations.sharedInstance.coordinates [Coordinate.Top] - cameraPlaneHeight;
		cameraPositionBounds [Coordinate.Left] = GridOperations.sharedInstance.coordinates [Coordinate.Left] + cameraPlaneWidth;
		cameraPositionBounds [Coordinate.Bottom] = GridOperations.sharedInstance.coordinates [Coordinate.Bottom] + cameraPlaneHeight;
		cameraPositionBounds [Coordinate.Right] = GridOperations.sharedInstance.coordinates [Coordinate.Right] - cameraPlaneWidth;

	}
		

	void Update () {
		if (Input.anyKey) {
			float offset = Time.deltaTime * cameraSpeed;
			if (Input.GetKey (KeyCode.W) && transform.position.y + offset <= cameraPositionBounds [Coordinate.Top]) {
				transform.Translate (0, offset, 0);
			}
			if (Input.GetKey (KeyCode.A) && transform.position.x - offset >= cameraPositionBounds [Coordinate.Left]) {
				transform.Translate (-offset, 0, 0);
			}
			if (Input.GetKey (KeyCode.S) && transform.position.y - offset >= cameraPositionBounds [Coordinate.Bottom]) {
				transform.Translate (0, -offset, 0);
			}
			if (Input.GetKey (KeyCode.D) && transform.position.x + offset <= cameraPositionBounds [Coordinate.Right]) {
				transform.Translate (offset, 0, 0);
			}
		}
	}
}
