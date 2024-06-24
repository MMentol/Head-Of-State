using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileType", menuName = "Grid Map/TileType", order = 0)]
public class TileType : ScriptableObject 
{
    public string tileTypeName;
    public Color tileColor;
    public bool isWalkable;
    public float level;
    public float spawnChance;
    public float adjacentChance;
}

