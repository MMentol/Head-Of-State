using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingTile", menuName = "Grid Map/BuildingTile", order = 0)]
public class BuildingTile : TileType
{
    public string buildingName;
    public int woodCost;
    public int stoneCost;
}
