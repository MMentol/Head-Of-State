using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureChooser : MonoBehaviour
{
    [SerializeField] public bool isBuildMode = false;
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
        if(_mouseIndicator != null && isBuildMode)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Tile tile = _gridManager.GetTileAtPos(new Vector2(_currentPos.x, _currentPos.y));
                Debug.Log("Tried to place.");
                if(tile.PlaceStructure(_mouseIndicator))
                {
                    DestroyCurrent();
                    Debug.Log($"Placed Structure at ({tile.position.x},{tile.position.y})");                    
                } else { Debug.Log("Failed Place");}
                isBuildMode = false;
            }
        }

        //Cancel (Default Keybind X)
        if(Input.GetKeyUp(KeyCode.X))
        {
            DestroyCurrent();
            Debug.Log("Cancel Select");
        }
    }

    public void ChooseStructure(GameObject chosenStructure)
    {
        DestroyCurrent();
        Debug.Log($"Select: {chosenStructure.name}");
        _mouseIndicator = Instantiate(chosenStructure, new Vector3(1000,1000,1000), Quaternion.identity, gameObject.transform);
        _mouseIndicator.name = $"{chosenStructure.name}";
    }

    public void ResetBuildMode()
    {
        isBuildMode = false;
        _currentPos = Vector2.zero;
    }

    void DestroyCurrent()
    {
        if (_mouseIndicator != null)
        {
            Destroy(_mouseIndicator);
            _mouseIndicator = null;
            ResetBuildMode();
        }
    }
}
