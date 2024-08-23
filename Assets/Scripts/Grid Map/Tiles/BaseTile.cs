using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseTile", menuName = "Grid Map/BaseTileType", order = 0)]
public class BaseTile : TileType, IComparable<BaseTile>
{
    public float level;
    public float spawnChance;
    public float adjacentChance;

    public int CompareTo(BaseTile otherType)
    {
        if (otherType.level != null)
            return this.level.CompareTo(otherType.level);
        else
            Debug.Log("Invalid Comparison", this);

        return 1;
    }
}
