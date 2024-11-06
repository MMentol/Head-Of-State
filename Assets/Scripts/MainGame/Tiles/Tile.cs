using GridMap.Resources;
using GridMap.Structures.Storage;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public bool logDebug = false;
    [Header("Tile Properties")]
    [SerializeField] private Sprite _baseSprite;
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    public TileType tileType {get ; private set;}
    public string initialType;

    [Header("Placement System Settings")]
    public MaterialDataStorage materialDataStorage;
    [SerializeField] public bool canPlaceOn = true;
    [SerializeField] public StructureChooser structureChooser;
    [SerializeField] private GameObject _mouseIndicator;
    [SerializeField] public bool noMoney = false;
    [SerializeField] public bool noSpace = false;

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
       
    public void Init(int x, int y, TileType type, bool logDebug = false)
    {
        this.materialDataStorage = MaterialDataStorage.Instance;
        this.logDebug = logDebug;
        position = new Vector2(x, y);
        tileType = type;
        initialType = tileType.tileTypeName;
        _baseSprite = tileType.tileSprite;
        _baseColor = tileType.tileColor;
        SetIsWalkable(tileType.isWalkable);
        if (_baseSprite != null)
        {
            _renderer.sprite = _baseSprite;
        }
        else 
            _renderer.color = _baseColor;

        if(initialType == "Water")
        {
            canPlaceOn = false;
            WaterResource water = gameObject.AddComponent<WaterResource>();
            water.maxBound = 999999;
            water.rawMaterialAmount = water.maxBound;

        }

    }

    //Mouse Events
    private void OnMouseEnter() 
    {
        if(structureChooser.isBuildMode)
        {
          structureChooser._currentPos = position;
        }

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
            if(structureChooser.isBuildMode == true)
            {
                _mouseIndicator = structureChooser._mouseIndicator;
            _mouseIndicator.transform.position = gameObject.transform.position;
            }
            else
            {
                structureChooser.isBuildMode = true;
            }
            
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
        //Debug.Log($"Position: ({position.x} , {position.y}) | Type: {tileType.tileTypeName} | Occupied : {isOccupied} ");
    }

    public bool PlaceStructure(GameObject placeableStructure, Structure strucProps)
    {
        this.materialDataStorage = MaterialDataStorage.Instance;
        if (!isOccupied && canPlaceOn)
        {
            if (materialDataStorage.DeductCosts(strucProps._woodCost, strucProps._stoneCost, strucProps._metalCost, strucProps._foodCost, strucProps._waterCost))
            {
                Debug.Log("Deducted Costs.");
                isOccupied = true;
                var placedObject = Instantiate(placeableStructure, gameObject.transform.position, Quaternion.identity, structureChooser._structTilemap.transform);
                _placedStructure = placedObject;
                Structure objectStructure = placedObject.GetComponent<Structure>();
                objectStructure.isPlaced = true;
                objectStructure._currentPos = position;
                objectStructure._tile = GetComponent<Tile>();
                placedObject.name = $"{placeableStructure.name} ({position.x},{position.y})";
                Structure placedStructure = placedObject.GetComponent<Structure>();
                placedStructure._woodSpent += placedStructure._woodCost;
                placedStructure._stoneSpent += placedStructure._stoneCost;
                placedStructure._metalSpent += placedStructure._metalCost;
                placedStructure._waterSpent += placedStructure._waterCost;
                placedStructure._foodSpent += placedStructure._foodCost;

                if (placedObject.TryGetComponent<MaterialStorageBase>(out var storageComponent))
                {
                    storageComponent.UpdateResources();
                }
                noMoney = false;
                noSpace = false;
                return true;
            }
            Debug.Log("Cant Afford");
            noMoney = true;
            return false;
        }
        Debug.Log("Occupied");
        noSpace = true;
        return false;
    }

    public bool PlaceResource(GameObject placeableStructure)
    {
        if (!isOccupied && canPlaceOn)
        {
            isOccupied = true;
            var placedObject = Instantiate(placeableStructure, gameObject.transform.position, Quaternion.identity, structureChooser._resourceTilemap.transform);
            _placedStructure = placedObject;
            Structure objectStructure = placedObject.GetComponent<Structure>();
            objectStructure.isPlaced = true;
            objectStructure._currentPos = position;
            objectStructure._tile = GetComponent<Tile>();
            placedObject.name = $"{placeableStructure.name} ({position.x},{position.y})";

            return true;
        }
        return false;
    }
    public bool DestroyStructure()
    {
        if (logDebug) { Debug.Log($"Destroying structure at ({position.x},{position.y})"); }
        if(isOccupied)
        {
            //Debug.Log($"Destroyed {_placedStructure.name}");
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
