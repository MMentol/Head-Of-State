using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Properties")]
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    [Header("Placement System Settings")]
    [SerializeField] public StructureChooser structureChooser;
    [SerializeField] private GameObject mouseIndicator;

    private Vector2 position;
    public TileType tileType {get ; private set;}
    public string initialType;
    public bool isResource = false;
    public HarvestableResource resourceType;
    public int resourceAmount;
    public bool isOccupied = false;


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
            resourceType = resourceTile.harvestableResource;
            resourceAmount = Random.Range(1200,25000);
        }
    }

    //Mouse Events
    private void OnMouseEnter() 
    {
        structureChooser.currentPos = gameObject.transform.position;

        if (structureChooser.mouseIndicator != null)
        {
            mouseIndicator = structureChooser.mouseIndicator;
            mouseIndicator.transform.position = gameObject.transform.position;
        }

        _highlight.SetActive(true);
    }
    private void OnMouseExit() 
    {
        if (mouseIndicator != null)
        {
            mouseIndicator = null;
        }
        _highlight.SetActive(false);
    }

    private void OnMouseUp() 
    {
        Debug.Log($"Position: ({position.x} , {position.y}) | Type: {tileType.tileTypeName} | Occupied : {isOccupied} "+ (isResource ? $"| Resource: {resourceType} Amount: {resourceAmount}" : "") );
    }
}
