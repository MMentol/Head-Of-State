using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureChooser : MonoBehaviour
{
    [SerializeField] public GameObject _mouseIndicator;
    [SerializeField] public GameObject[] _placeableObjects;
    [SerializeField] public Vector2 _currentPos;

    [Header("Grid")]
    [SerializeField] private GridManager _gridManager;
    [SerializeField] public Tilemap _tilemap;


    void Update() 
    {
        if(_currentPos == null)
        {
            _currentPos = Vector2.zero;
        }

        //Switch between objects
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            DestroyCurrent();
            Debug.Log($"Select: {_placeableObjects[0].name}");
            _mouseIndicator = Instantiate(_placeableObjects[0], _currentPos, Quaternion.identity, gameObject.transform);
            _mouseIndicator.name = $"{_placeableObjects[0].name}";
        }
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            DestroyCurrent();
            Debug.Log($"Select: {_placeableObjects[1].name}");
            _mouseIndicator = Instantiate(_placeableObjects[1], _currentPos, Quaternion.identity, gameObject.transform);
            _mouseIndicator.name = $"{_placeableObjects[1].name}";
        }

        //Place
        if(_mouseIndicator != null)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Tile tile = _gridManager.GetTileAtPos(new Vector2(_currentPos.x, _currentPos.y));
                if(tile.PlaceStructure(_mouseIndicator))
                {
                    DestroyCurrent();
                    Debug.Log($"Placed Structure at ({tile.position.x},{tile.position.y})");
                } else { Debug.Log("Failed Place"); }
            }
        }

        //Cancel
        if(Input.GetKeyUp(KeyCode.X))
        {
            DestroyCurrent();
            Debug.Log("Cancel Select");
        }
    }

    void DestroyCurrent()
    {
        if (_mouseIndicator != null)
        {
            Destroy(_mouseIndicator);
            _mouseIndicator = null;
        }
    }
}
