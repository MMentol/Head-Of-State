using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Properties")]
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    public TileType tileType {get ; private set;}
    public string initialType;
    public bool isResource = false;
    public HarvestableResource resourceType;
    public Inventory inventory;
    public int resourceAmount;

    [Header("Placement System Settings")]
    [SerializeField] public StructureChooser structureChooser;
    [SerializeField] private GameObject _mouseIndicator;

    public Vector2 position;

    [Header("Placed Structure")]
    public bool isOccupied = false;
    public GameObject _placedStructure = null;
    
    [Header("Pathfinding")]
    public int gCost;
    public int hCost;
    public int fCost;
    
    public bool isWalkable;
    public Tile cameFromTile;
       
    public void Init(int x, int y, TileType type)
    {
        position = new Vector2(x, y);
        tileType = type;
        initialType = tileType.tileTypeName;
        _baseColor = tileType.tileColor;
        _renderer.color = _baseColor;
        isResource = (type is ResourceTile);

        if(isResource)
        {   
            ResourceTile resourceTile = (ResourceTile) tileType;
            inventory = gameObject.AddComponent<Inventory>();
            inventory.Init(resourceTile.harvestableResource.ToString(), Random.Range(1200,2500));            
            resourceType = resourceTile.harvestableResource;
        }
    }

    //Mouse Events
    private void OnMouseEnter() 
    {
        structureChooser._currentPos = position;

        if (structureChooser._mouseIndicator != null)
        {
            _mouseIndicator = structureChooser._mouseIndicator;
            _mouseIndicator.transform.position = gameObject.transform.position;
        }

        _highlight.SetActive(true);
    }

    private void OnMouseOver() 
    {
        if (structureChooser._mouseIndicator != null)
        {
            _mouseIndicator = structureChooser._mouseIndicator;
            _mouseIndicator.transform.position = gameObject.transform.position;
        }
    }

    private void OnMouseExit() 
    {
        if (_mouseIndicator != null)
        {
            _mouseIndicator = null;
        }
        _highlight.SetActive(false);
    }

    private void OnMouseUp() 
    {
        Debug.Log($"Position: ({position.x} , {position.y}) | Type: {tileType.tileTypeName} | Occupied : {isOccupied} "+ (isResource ? $"| Resource: {resourceType.ToString()}, Amount: {inventory._resources[resourceType.ToString()]}" : "") );
    }

    public bool PlaceStructure(GameObject placeableStructure)
    {
        if(!isOccupied)
        {
            isOccupied = true;
            var placedObject =  Instantiate(placeableStructure, gameObject.transform.position, Quaternion.identity, structureChooser._tilemap.transform);
            _placedStructure = placedObject;
            Structure objectStructure = placedObject.GetComponent<Structure>();
            objectStructure.isPlaced = true;
            objectStructure._currentPos = position;
            objectStructure._tile = GetComponent<Tile>();
            placedObject.name = $"{placeableStructure.name} ({position.x},{position.y})";
            return true;
        }
        Debug.Log("Occupied");
        return false;
    }

    public bool DestroyStructure()
    {
        Debug.Log($"Destroying structure at ({position.x},{position.y})");
        if(isOccupied)
        {
            Destroy(_placedStructure);
            _placedStructure = null;
            isOccupied = false;
            return true;
        }
        return false;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }
}
