using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    void Start () 
    {
        int seed = Random.Range(-1000000,1000000);
        Debug.Log($"Seed: {seed}");
        GenerateGrid(seed);
    }
    [Header("Map Size")]
    [SerializeField] private int _width, _height;

    [Header("Base Tile")]
    [SerializeField] private Tile _tilePrefab;
    //[SerializeField] private Transform _camTransform;

    [Header("Grid")]
    [SerializeField] private Tilemap _tilemap;
    
    [SerializeField] private Dictionary<Vector2, Tile> _tiles;

    [Header("Tile Types")]
    [SerializeField] private TileType[] _baseTileTypes;
    [SerializeField] private TileType _forestType;

    void GenerateGrid(int seed) {
        //Debug Toggle
        bool logDebug = false;

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
                var spawnedTile = Instantiate(_tilePrefab, worldPos, Quaternion.identity);
                //spawnedTile.Init(GenerateTileType(x, y));
                spawnedTile.Init(x, y, GenerateTileTypeFromNoise(noiseMap, x, y));
                spawnedTile.name = $"Tile ({x} , {y}) [{spawnedTile.initialType}]";
                if(logDebug) {Debug.Log("Tile type: " + spawnedTile.initialType);}
                _tiles[new Vector2(x,y)] = spawnedTile;
            }
        }

        //Forest Generator
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector2 currentPos = new Vector2(x , y);
                Tile currentTile = GetTileAtPos(currentPos);
                if (currentTile.initialType == "Grass")
                {
                    if(Random.value > 0.80)
                    {
                        currentTile.Init(x, y, _forestType);
                        currentTile.name = $"Tile ({x} , {y}) [{currentTile.initialType}]";
                    }
                }
                
            }
        }

        //_camTransform.position = new Vector3((float) _width / 2 -0.5f, (float) _height / 2 -0.5f, -10);
    }

    //Get Tile from Dictionary
    public Tile GetTileAtPos(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
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
        foreach (TileType type in _baseTileTypes)
        {
            if(noiseValue < type.level)
            {
                return type;
            }

        }

        return _baseTileTypes[_baseTileTypes.Length - 1];
    }


    //Not Currently Used
    TileType GenerateTileType(int x, int y, bool logDebug = false)
    {
        float downChance = Random.value;
        float leftChance = Random.value;
        int randomType = Random.Range(0, _baseTileTypes.Length);
        if(logDebug) {Debug.Log($"Checking {x} , {y} | Chances: {leftChance} , {downChance}");}

        if(y > 0 && downChance > leftChance)
        {
            TileType down = GetTileAtPos(new Vector2(x, y - 1)).GetTileType();
            if(logDebug) {Debug.Log($"Down: {down.tileTypeName}");}
            if (down.adjacentChance >  downChance)
            {
                return down;
            }
        }

        if(x > 0 && downChance < leftChance)
        {
            TileType left = GetTileAtPos(new Vector2(x - 1, y)).GetTileType();
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
    }
}
