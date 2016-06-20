using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGraph : MonoBehaviour
{
    public Node[,] graph;

    public Node goal;

    private static MapGraph _sharedInstance;
    public static MapGraph sharedInstance
    {
        get
        {
            if (!_sharedInstance)
            {
                _sharedInstance = GameObject.FindObjectOfType<MapGraph>();
                _sharedInstance.CreateGrid();
            }
            return _sharedInstance;
        }
    }

    void CreateGrid()
    {
        float nodeWidth = GridOperations.sharedInstance.cellWidth;
        float nodeHeight = GridOperations.sharedInstance.cellHeight;

        int numberOfRows = GridOperations.sharedInstance.grid.GetLength(1);
        int numberOfColumns = GridOperations.sharedInstance.grid.GetLength(0);

        graph = new Node[numberOfRows, numberOfColumns];
        Vector2 mapTopLeft = new Vector2(GridOperations.sharedInstance.coordinates[Coordinate.Left], GridOperations.sharedInstance.coordinates[Coordinate.Top]);

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                Vector2 worldPoint = mapTopLeft + new Vector2(j*nodeWidth+nodeWidth/2,-i*nodeHeight - nodeHeight/2);
                graph[i, j] = new Node(worldPoint, (TileType)GridOperations.sharedInstance.grid[i, j], i, j);
                if((TileType)GridOperations.sharedInstance.grid[i, j] == TileType.Treasure)
                {
                    goal = graph[i, j];
                }
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                int checkRow = node.gridRow + i;
                int checkColumn = node.gridColumn + j;

                if (checkRow >= 0 && checkRow < graph.GetLength(0) && checkColumn >= 0 && checkColumn < graph.GetLength(1))
                {
                    neighbours.Add(graph[checkRow, checkColumn]);
                }
            }
        }

        return neighbours;
    }


    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + GridOperations.sharedInstance.mapWidth / 2) / GridOperations.sharedInstance.mapWidth;
        float percentY = (worldPosition.y + GridOperations.sharedInstance.mapHeight / 2) / GridOperations.sharedInstance.mapHeight;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((graph.GetLength(0) - 1) * percentX);
        int y = Mathf.RoundToInt((graph.GetLength(1) - 1) * percentY);
        return graph[x, y];
    }
}
