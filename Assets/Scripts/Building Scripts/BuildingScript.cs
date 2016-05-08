using UnityEngine;
using System.Collections;
using System.Linq;

public class BuildingScript : MonoBehaviour {
	public int baseCellsWidth;
	public int baseCellsHeight;

	public float movementSpeed;

	[SerializeField]
	private bool isStatic;
	[SerializeField]
	private float noOfCellsHigh = 0.25f;

	private int topLeftCellX;
	private int topLeftCellY;
	private bool canPlace;
	private SpriteRenderer mRenderer;

	// Use this for initialization
	void Start () {
		mRenderer = gameObject.GetComponent<SpriteRenderer> ();
		rescaleSprite ();
		isStatic = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (!isStatic) {
			if (Input.GetMouseButtonDown (0) && canPlace) {
				isStatic = true;
				GridOperations.sharedInstance.WriteBuildingToGrid (topLeftCellX, topLeftCellY, baseCellsWidth, baseCellsHeight);
				mRenderer.color = Color.white;
			} else {
				followCursor ();
			}
		}
	}

	private void changeColorForPlacement () {
		canPlace = GridOperations.sharedInstance.CheckFreeSpace (topLeftCellX, topLeftCellY, baseCellsWidth, baseCellsHeight);
		if (canPlace) {
			mRenderer.color = new Color (198.0f / 255.0f, 238.0f / 255.0f, 196.0f / 255.0f);
		} else {
			mRenderer.color = new Color (238.0f / 255.0f, 161.0f / 255.0f, 188.0f / 255.0f);
		}
	}

	private void followCursor() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		float cursorColumn = Mathf.Abs(mousePosition.x - GridOperations.sharedInstance.coordinates [Coordinate.Left]) / GridOperations.sharedInstance.cellWidth; 
		float cursorRow = Mathf.Abs (GridOperations.sharedInstance.coordinates [Coordinate.Top] - mousePosition.y) / GridOperations.sharedInstance.cellHeight;

		if ((int)cursorColumn != topLeftCellX || (int)cursorRow != topLeftCellY) {
			topLeftCellX = Mathf.FloorToInt (cursorColumn);
			topLeftCellY = Mathf.FloorToInt (cursorRow);

			float pDisplacedX = cursorColumn - topLeftCellX;
			float pDisplacedY = cursorRow - topLeftCellY;

			float centerX = mousePosition.x + GridOperations.sharedInstance.cellWidth * ((float)baseCellsWidth / 2.0f - pDisplacedX);
			float centerY = mousePosition.y + GridOperations.sharedInstance.cellHeight * (-(float)baseCellsHeight / 2.0f + pDisplacedY + noOfCellsHigh / 2.0f);

			float scaledHeight = (centerY - GridOperations.sharedInstance.coordinates [Coordinate.Top]) / GridOperations.sharedInstance.mapHeight;

			transform.position = new Vector3 (centerX, centerY, scaledHeight);
			changeColorForPlacement ();
		}
	}

	private void rescaleSprite() {
		float mSpriteWidth = mRenderer.sprite.bounds.size.x;
		float mSpriteHeight = mRenderer.sprite.bounds.size.y;

		float scaleH = (baseCellsWidth*GridOperations.sharedInstance.cellWidth)/mSpriteWidth;
		float scaleV = ((baseCellsHeight+noOfCellsHigh)*GridOperations.sharedInstance.cellHeight)/mSpriteHeight;

		transform.localScale = new Vector3 (scaleH, scaleV, 1.0f);
	}
}
