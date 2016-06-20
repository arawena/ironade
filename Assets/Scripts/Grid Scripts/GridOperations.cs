using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TileType
{
    Clear = 0,
    Path = 1,
    Building = 2,
    Treasure = 3
};

public class GridOperations : MonoBehaviour {

	[SerializeField]
	private string fileName = "gridSettings";

    private Renderer mRenderer;

	private static GridOperations _sharedInstance;

	public int[,] grid;

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

	public void WriteBuildingToGrid(int topCellIndexX, int topCellIndexY, int width, int height) {
        for (int i = topCellIndexY; i < topCellIndexY + height; i++) {
            for (int j = topCellIndexX; j < topCellIndexX + width; j++) {
                grid [i, j] = (int)TileType.Building;
			}
		}
	}

	public bool CheckFreeSpace(int topCellIndexX, int topCellIndexY, int width, int height) {
		for (int i = topCellIndexY; i < topCellIndexY + height; i++) {
			for (int j = topCellIndexX; j < topCellIndexX + width; j++) {
				if (grid [i, j] != 0) {
					return false;
				}
			}
		}
		return true;
	}

    public List<Vector2> GetSpawnLocations()
    {
        int firstRow = 0;
        int lastRow = grid.GetLength(1)-1;
        List<Vector2> spawnLocations = new List<Vector2>(); 

        for(int j=0; j<grid.GetLength(0); j++)
        {
            if(grid[firstRow, j] == (int)TileType.Path) {
                spawnLocations.Add(new Vector2(firstRow,j));
            }
            if (grid[lastRow, j] == (int)TileType.Path)
            {
                spawnLocations.Add(new Vector2(lastRow, j));
            }
        }
        int lastColumn = grid.GetLength(0)-1;
        for (int j = 1; j < grid.GetLength(1)-1; j++)
        {
            if (grid[j,firstRow] == (int)TileType.Path)
            {
                spawnLocations.Add(new Vector2(j, firstRow));
            }
            if (grid[j, lastColumn] == (int)TileType.Path)
            {
                spawnLocations.Add(new Vector2(j, lastColumn));
            }
        }

        return spawnLocations;
    }
}
