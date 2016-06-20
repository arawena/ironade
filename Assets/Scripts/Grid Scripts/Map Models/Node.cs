using UnityEngine;
using System.Collections;

public class Node
{
    public Vector2 centerWorldPos;
    public TileType type;
    public int gridRow;
    public int gridColumn;
    public int gCost;
    public int hCost;

    public Node parent;

    public Node(Vector2 nodeCenterWorldPos, TileType nodeType, int nodeRow, int nodeColumn)
    {
        centerWorldPos = nodeCenterWorldPos;
        type = nodeType;
        gridRow = nodeRow;
        gridColumn = nodeColumn;
    }

    public int GetFCost()
    {
        return gCost + hCost;
    }
}
