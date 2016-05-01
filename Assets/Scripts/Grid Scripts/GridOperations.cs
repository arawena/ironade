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

	public float topCoordinate{ get; private set;}
	public float leftCoordinate{ get; private set;}



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
		this.mapWidth = mRenderer.bounds.size.x;
		this.mapHeight = mRenderer.bounds.size.y;

		this.cellHeight = (int)mapWidth / grid.GetLength (0);
		this.cellWidth = (int)mapHeight / grid.GetLength (1);

		this.topCoordinate = mRenderer.transform.position.y + mapHeight / 2;
		this.leftCoordinate = mRenderer.transform.position.x - mapWidth / 2;
	}

}
