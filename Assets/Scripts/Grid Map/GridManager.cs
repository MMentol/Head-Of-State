using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    //Debug Toggle
    [SerializeField] public bool logDebug = false;
    [Header("Map Size")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [Header("Base Tile")]
    [SerializeField] private Tile _tilePrefab;
    //[SerializeField] private Transform _camTransform;

    [Header("Grid")]
    [SerializeField] private Tilemap _tilemap;
    
    [SerializeField] public Dictionary<Vector2, Tile> _tiles;

    [Header("Tile Types")]
    [SerializeField] private BaseTile[] _baseTileTypes;
    [SerializeField] private ResourceNode[] _resourceTypes;

    [Header("Map Settings")]

    [Header("Placement System Settings")]
    [SerializeField] private StructureChooser structureChooser;

    #region Lifecycle Methods
    void Awake()
    {
        
        Array.Sort(_baseTileTypes);
    }
    void Start () 
    {       
        int seed = Random.Range(-1000000,1000000);
        Debug.Log($"Seed: {seed}");
        GenerateGrid(seed);
    }
    #endregion

    #region Generator Methods
    void GenerateGrid(int seed) {
        //Tile Storage
        _tiles = new Dictionary<Vector2, Tile>();

        //Noise Map
        float[,] noiseMap = GenerateNoiseMap(_width, _height, seed);

        //Tile Generator
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3Int currentPos = new Vector3Int(x , y ,0);
                Vector3 worldPos = _tilemap.CellToWorld(currentPos);
                var spawnedTile = Instantiate(_tilePrefab, worldPos, Quaternion.identity, _tilemap.transform);
                //spawnedTile.Init(GenerateTileType(x, y));
                spawnedTile.Init(x, y, GenerateTileTypeFromNoise(noiseMap, x, y));
                spawnedTile.name = $"Tile ({x} , {y}) [{spawnedTile.initialType}]";
                Tile tileSettings = spawnedTile.GetComponent<Tile>();
                tileSettings.structureChooser = structureChooser;
                if(logDebug) {Debug.Log("Tile type: " + spawnedTile.initialType);}
                _tiles[new Vector2(x,y)] = spawnedTile;
            }
        }

        //Resource Generation
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if(GenerateResourceNode(x,y, false))
                {if(logDebug){Debug.Log($"Placed Resource. at {x},{y}");}};
            }
        }

        //_camTransform.position = new Vector3((float) _width / 2 -0.5f, (float) _height / 2 -0.5f, -10);
    }

    //Generate NoiseMap based from Seed
    float[,] GenerateNoiseMap(int _width, int _height, int seed = 0)
    {
        float scale = 0.1f;
        float[,] noiseMap = new float[_width , _height];
        for(int y = 0; y < _height; y++)
        {
            for(int x = 0; x < _width; x++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(x * scale + seed, y * scale + seed);
            }
        }
        Debug.Log("Noise Map Done.");
        return noiseMap;
    }

    //Generate TileType based on NoiseMap
    TileType GenerateTileTypeFromNoise(float[,] noiseMap, int x, int y, bool logDebug = false)
    {
        float noiseValue = noiseMap[x,y];
        if(logDebug) {Debug.Log($"{x} , {y} | Noise: {noiseValue}");}
        foreach (BaseTile type in _baseTileTypes)
        {
            if(noiseValue < type.level)
            {
                return type;
            }

        }

        return _baseTileTypes[_baseTileTypes.Length - 1];
    }

    //Resource Generator
    bool GenerateResourceNode(int x, int y, bool logDebug = false)
    {
        Vector2 currentPos = new Vector2(x, y);
        Tile currentTile = GetTileAtPos(currentPos);
        var curType = (BaseTile)currentTile.tileType;

        float totalChance = 0f;
        List<ResourceNode> matchingNodes = new List<ResourceNode>();

        // Find all ResourceNodes with matching baseType
        foreach (ResourceNode resourceNode in _resourceTypes)
        {
            if (curType == resourceNode._baseType)
            {
                matchingNodes.Add(resourceNode);
                totalChance += resourceNode.nodeChance;
            }
        }

        // Check if any matching nodes exist
        if (matchingNodes.Count == 0)
        {
            return false;
        }

        float rand = Random.value;
        //Debug.Log(rand);

        // Check if a resource node should be generated based on combined probability
        if (rand <= totalChance)
        {
            // Choose a resource node based on their individual chances (weighted random selection)
            float accumulatedChance = 0f;
            int chosenIndex = 0;
            while (accumulatedChance < rand)
            {
                accumulatedChance += matchingNodes[chosenIndex].nodeChance;
                chosenIndex++;
            }

            ResourceNode chosenNode = matchingNodes[chosenIndex - 1];

            currentTile.PlaceStructure(chosenNode.gameObject);
            return true;
        }

        return false;               
    }

    //Not Currently Used
    /*TileType GenerateTileType(int x, int y, bool logDebug = false)
    {
        float downChance = Random.value;
        float leftChance = Random.value;
        int randomType = Random.Range(0, _baseTileTypes.Length);
        if(logDebug) {Debug.Log($"Checking {x} , {y} | Chances: {leftChance} , {downChance}");}

        if(y > 0 && downChance > leftChance)
        {
            BaseTile down = (BaseTile) GetTileAtPos(new Vector2(x, y - 1)).tileType;
            if(logDebug) {Debug.Log($"Down: {down.tileTypeName}");}
            if (down.adjacentChance >  downChance)
            {
                return down;
            }
        }

        if(x > 0 && downChance < leftChance)
        {
            BaseTile left = (BaseTile) GetTileAtPos(new Vector2(x - 1, y)).tileType;
            if(logDebug) {Debug.Log($"Left: {left.tileTypeName}");}
            if(left.adjacentChance > leftChance)
            {
                return left;
            }
        }

        if(logDebug) {Debug.Log("Generating Random Type");}
        while (Random.value < _baseTileTypes[randomType].spawnChance)
        {
            randomType = Random.Range(0, _baseTileTypes.Length);
        }
        return _baseTileTypes[randomType];
    }*/
    #endregion

    #region Tile Methods
    //Get Tile from Dictionary
    public Tile GetTileAtPos(Vector2 pos, bool logDebug = false)
    {
        if(logDebug) {Debug.Log($"Getting tile: {_tiles[pos].name}");}
        return _tiles[pos];
    }
    #endregion

    #region Misc Utility
    #endregion

    #region Getters
    public int GetWidth()
    {
        return _width;
    }
    public int GetHeight()
    {
        return _height;
    }

    
    #endregion

}
