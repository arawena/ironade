using UnityEngine;
using System.Collections;

public class Node
{
    public Vector3 centerWorldPos;
    public TileType type;
    public int gridRow;
    public int gridColumn;
    public int gCost;
    public int hCost;

    public int deathToll = 0;

    public Node parent;

    public Node(Vector2 nodeCenterWorldPos, TileType nodeType, int nodeRow, int nodeColumn)
    {
        float zPosition = (nodeCenterWorldPos.y - GridOperations.sharedInstance.coordinates[Coordinate.Top]) / GridOperations.sharedInstance.mapHeight;
        centerWorldPos = new Vector3(nodeCenterWorldPos.x, nodeCenterWorldPos.y, zPosition);
        type = nodeType;
        gridRow = nodeRow;
        gridColumn = nodeColumn;
    }

    public int GetFCost()
    {
        return gCost + hCost + (deathToll>0?(10-10/deathToll):0);
    }

    public Vector3 getOuterMostCoordinate()
    {
        if(gridRow == 0)
        {
            return centerWorldPos + new Vector3(0, GridOperations.sharedInstance.cellHeight / 2, (GridOperations.sharedInstance.cellHeight / (2 * GridOperations.sharedInstance.mapHeight)));
        } else if(gridColumn == 0)
        {
            return centerWorldPos - new Vector3(GridOperations.sharedInstance.cellWidth / 2, 0, 0);
        } else if(gridRow >= gridColumn)
        {
            return centerWorldPos - new Vector3(0, GridOperations.sharedInstance.cellHeight / 2, (GridOperations.sharedInstance.cellHeight/ (2*GridOperations.sharedInstance.mapHeight)));
        } else
        {
            return centerWorldPos + new Vector3(GridOperations.sharedInstance.cellWidth / 2, 0, 0);
        }
    }
}
