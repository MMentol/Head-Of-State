using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Properties")]
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private Vector2 position;
    public TileType tileType {get ; private set;}
    public string initialType;
    public bool isResource = false;
    public HarvestableResource resourceType;
    public int resourceAmount;


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
        _highlight.SetActive(true);
    }
    private void OnMouseExit() 
    {
        _highlight.SetActive(false);
    }

    private void OnMouseUp() 
    {
        Debug.Log($"Position: ({position.x} , {position.y}) | Type: {tileType.tileTypeName}" + (isResource ? $"| Resource: {resourceType} Amount: {resourceAmount}" : "") );
    }
}
