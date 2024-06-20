using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public TileType tileType;
    [SerializeField] public string initialType;
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public void Init(TileType type)
    {
        tileType = type;
        initialType = tileType.tileTypeName;
        _baseColor = tileType.tileColor;
        _renderer.color = _baseColor;
    }

    private void OnMouseEnter() 
    {
        _highlight.SetActive(true);
    }
    private void OnMouseExit() 
    {
        _highlight.SetActive(false);
    }

    public TileType GetTileType()
    {
        return tileType;
    }
}
