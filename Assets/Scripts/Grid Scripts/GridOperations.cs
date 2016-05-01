using UnityEngine;
using System.Collections;

public class GridOperations : MonoBehaviour {

	[SerializeField]
	private string fileName = "gridSettings";

	private Renderer mRenderer;

	private static GridOperations _sharedInstance;

	private int[,] grid;

	public float mapWidth { get; private set;}
	public float mapHeight{ get; private set;}

	public float cellHeight{ get; private set;}
	public float cellWidth{ get;private set;}

	public float[] coordinates{ get; private set;}

	public static GridOperations sharedInstance {
		get {
			if(!_sharedInstance) {
				_sharedInstance = GameObject.FindObjectOfType<GridOperations>();
				_sharedInstance.Init ();
			}
			return _sharedInstance;
		}
	}

	public void Init () {
		grid = FileOperations.ReadFile (fileName);
		mRenderer = GetComponent<Renderer> ();
		this.ChangeGridDimensions ();
	}

	public void ChangeGridDimensions () {
		mapWidth = mRenderer.bounds.size.x;
		mapHeight = mRenderer.bounds.size.y;

		cellHeight = mapWidth / grid.GetLength (0);
		cellWidth = mapHeight / grid.GetLength (1);

		coordinates = new float[4];
		coordinates[Coordinate.Top] = mRenderer.transform.position.y + mapHeight / 2;
		coordinates[Coordinate.Left] = mRenderer.transform.position.x - mapWidth / 2;
		coordinates[Coordinate.Bottom] = mRenderer.transform.position.y - mapHeight / 2;
		coordinates[Coordinate.Right] = mRenderer.transform.position.x + mapWidth / 2;
	}

}
