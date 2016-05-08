using UnityEngine;
using System.Collections;
using System.Linq;

public class BuildingScript : MonoBehaviour {
	public int baseCellsWidth;
	public int baseCellsHeight;

	[SerializeField]
	private float noOfCellsHigh = 0.25f;

	public float movementSpeed;

	[SerializeField]
	private bool isStatic;

	// Use this for initialization
	void Start () {
		rescaleSprite ();
		isStatic = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (!isStatic) {
			followCursor ();
			if (Input.GetMouseButtonDown (0)) {
				isStatic = true;
			}
		}
	}

	private void followCursor() {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		float cursorColumn = Mathf.Abs(mousePosition.x - GridOperations.sharedInstance.coordinates [Coordinate.Left]) / GridOperations.sharedInstance.cellWidth; 
		float cursorRow = Mathf.Abs (mousePosition.y - GridOperations.sharedInstance.coordinates [Coordinate.Bottom]) / GridOperations.sharedInstance.cellHeight;

		float pDisplacedX = cursorColumn - Mathf.Floor (cursorColumn);
		float pDisplacedY = cursorRow - Mathf.Floor (cursorRow);

		float centerX = mousePosition.x + GridOperations.sharedInstance.cellWidth * ((float)baseCellsWidth / 2.0f - pDisplacedX);
		float centerY = mousePosition.y + GridOperations.sharedInstance.cellHeight * ((float)baseCellsHeight / 2.0f - pDisplacedY + noOfCellsHigh/2.0f);

		float scaledHeight = (centerY - GridOperations.sharedInstance.coordinates [Coordinate.Top]) / GridOperations.sharedInstance.mapHeight;
		transform.position = new Vector3 (centerX, centerY, scaledHeight);
	}

	private void rescaleSprite() {
		SpriteRenderer mRenderer = gameObject.GetComponent<SpriteRenderer> ();

		float mSpriteWidth = mRenderer.sprite.bounds.size.x;
		float mSpriteHeight = mRenderer.sprite.bounds.size.y;

		float scaleH = (baseCellsWidth*GridOperations.sharedInstance.cellWidth)/mSpriteWidth;
		float scaleV = ((baseCellsHeight+noOfCellsHigh)*GridOperations.sharedInstance.cellHeight)/mSpriteHeight;

		transform.localScale = new Vector3 (scaleH, scaleV, 1.0f);
	}
}
