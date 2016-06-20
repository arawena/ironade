using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

    MapGraph grid;

    private const int diagonalWeight = 14;
    private const int perpWeight = 10;

    public Dictionary<Node, List<Node>> pathsForSpawnLocations;

    void Start()
    {
        grid = MapGraph.sharedInstance;
        pathsForSpawnLocations = new Dictionary<Node, List<Node>>();
        CalculatePathsForSpawnLocation(new Vector2(0,0));
    }

    void CalculatePathsForSpawnLocation(Vector2 sLocation)
    {
        Node startNode = grid.graph[(int)sLocation.x, (int)sLocation.y];
        FindPath(startNode, grid.goal);
    }

    void FindPath(Node startNode, Node targetNode)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].GetFCost() < currentNode.GetFCost() || openSet[i].GetFCost() == currentNode.GetFCost() && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if ((neighbour.type != TileType.Path && neighbour.type != TileType.Treasure) || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        pathsForSpawnLocations.Add(startNode, path);
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridColumn - nodeB.gridColumn);
        int dstY = Mathf.Abs(nodeA.gridRow - nodeB.gridRow);

        if (dstX > dstY)
            return diagonalWeight * dstY + perpWeight * (dstX - dstY);
        return diagonalWeight * dstX + perpWeight * (dstY - dstX);
    }
}
