using UnityEngine;
using System.Collections;

public class GridSettingsScript : MonoBehaviour
{

    void Start()
    {
        Renderer mRenderer = gameObject.GetComponent<Renderer>();
        Material mMaterial = mRenderer.material;
        mMaterial.SetFloat("_GridSpacingX", GridOperations.sharedInstance.cellWidth);
        mMaterial.SetFloat("_GridSpacingY", GridOperations.sharedInstance.cellHeight);

        int numberOfColumns = GridOperations.sharedInstance.grid.GetLength(1);
        mMaterial.SetInt("_NumberOfColumns", numberOfColumns);
        mMaterial.SetVector("_LeftCoordinates", new Vector2(GridOperations.sharedInstance.coordinates[Coordinate.Left], GridOperations.sharedInstance.coordinates[Coordinate.Top]));
        int coordinate;
        for (int i = 0; i < GridOperations.sharedInstance.grid.GetLength(0); i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                coordinate = i * numberOfColumns + j;
                mMaterial.SetVector("_RoadPoints" + coordinate.ToString(), new Vector2(GridOperations.sharedInstance.grid[i, j],0));
            }
        }
    }

}
