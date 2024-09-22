using GridMap.Structures.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureChooser : MonoBehaviour
{
    public bool isBuildMode = false;
    public GameObject _mouseIndicator;
    public GameObject _storedStructure;
    public Structure _structureProperties;
    public GameObject[] _placeableObjects;
    public Vector2 _currentPos;

    [Header("Grid")]
    [SerializeField] private GridManager _gridManager;
    public Tilemap _tilemap;


    void Update() 
    {
        if(_currentPos == null)
        {
            _currentPos = Vector2.zero;
        }

        //Switch between objects
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            ChooseStructure(_placeableObjects[0]);
            
        }
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            ChooseStructure(_placeableObjects[1]);           
        }

        //Place
        if(_mouseIndicator != null && isBuildMode)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Tile tile = _gridManager.GetTileAtPos(new Vector2(_currentPos.x, _currentPos.y));
                Debug.Log("Tried to place.");
                if(tile.PlaceStructure(_storedStructure, _structureProperties))
                {                    
                    DestroyCurrent();
                    Debug.Log($"Placed Structure at ({tile.position.x},{tile.position.y})");                    
                } else { Debug.Log("Failed Place");}
                DestroyCurrent();
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
        _storedStructure = chosenStructure;
        Debug.Log($"Select: {chosenStructure.name}");
        _mouseIndicator = Instantiate(chosenStructure, new Vector3(1000,1000,1000), Quaternion.identity, gameObject.transform);
        _structureProperties = _mouseIndicator.GetComponent<Structure>();
        _mouseIndicator.name = $"CURSOR: {chosenStructure.name}";
        if (_mouseIndicator.TryGetComponent<MaterialStorageBase>(out var storageComponent))
        {
            Destroy(storageComponent);
        }

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
            _storedStructure = null;
            _mouseIndicator = null;
            ResetBuildMode();
            FindObjectOfType<MaterialDataStorage>().TallyMaterials();
        }
    }
}
