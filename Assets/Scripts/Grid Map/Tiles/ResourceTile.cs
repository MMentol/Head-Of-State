using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HarvestableResource
{
    Wood,
    Stone,
    Food,
    Water
}

[CreateAssetMenu(fileName = "ResourceTile", menuName = "Grid Map/ResourceTile", order = 0)]
public class ResourceTile : TileType
{
    public HarvestableResource harvestableResource;
    public float nodeChance;
}
