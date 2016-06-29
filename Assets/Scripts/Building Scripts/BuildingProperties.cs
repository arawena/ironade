using UnityEngine;
using System.Collections;

public class BuildingProperties:MonoBehaviour {

    public int baseCellsWidth;
    public int baseCellsHeight;

    [SerializeField]
    public float noOfCellsHigh = 0.25f;

    public int topLeftCellX;
    public int topLeftCellY;

    public float rangeRadius;
    public float firingSpeed;
}
