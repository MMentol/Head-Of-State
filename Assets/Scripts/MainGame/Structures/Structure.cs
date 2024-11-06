using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GridMap.Structures.Storage;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    readonly bool logDebug = false;
    [Header("Structure Properties")]
    public string name = "Structure Name";
    public bool isPlaced = false;
    public bool isRemovable = true;
    public Vector2 _currentPos;
    public Tile _tile;
    public float returnPercentage = 0.50f;

    [Header("Resource Placement Costs")]
    public int _woodCost = 0;
    public int _stoneCost = 0;
    public int _metalCost = 0;
    public int _foodCost = 0;
    public int _waterCost = 0;

    [Header("Resources Spent")]
    public int _woodSpent = 0;
    public int _stoneSpent = 0;
    public int _metalSpent = 0;
    public int _foodSpent = 0;
    public int _waterSpent = 0;

    [Header("On-Click UI")]
    GameObject ocUI;
    GameObject realUI;

    /*void Awake(){
        this.ocUI = GameObject.FindWithTag("BuildingUI");
        realUI = ocUI.transform.GetChild(10).GetChild(0).gameObject;
        StructNameTxt = realUI.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        ResidentsTxt = realUI.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        StorageTxt = realUI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        UpgradeButton = realUI.transform.GetChild(3).gameObject.GetComponent<Button>();
        DemolishButton = realUI.transform.GetChild(4).gameObject.GetComponent<Button>();
        stor = gameObject.GetComponent<MaterialStorageBase>();
        house = gameObject.GetComponent<House>();
    }*/
    void OnMouseOver() {
        if(isPlaced)
        {
            if(Input.GetMouseButtonUp(1) && logDebug)
            {
                //Debug.Log("Rightclick structure");
                if(isRemovable)
                {
                    _tile.DestroyStructure();
                }
            }
            if(Input.GetMouseButtonUp(0)) {
                // Make UI Open here
                if(isRemovable) {
                    //GameObject.FindWithTag("BuildingUI").transform.GetChild(10).gameObject.SetActive(true);
                    StructureWindow.Instance.SetStructure(this);
                    StructureWindow.Instance.ShowWindow();
                }
            }
        }
    }

    //Returns on demolish
    private void OnDestroy()
    {
        MaterialDataStorage dataStorage = MaterialDataStorage.Instance;

        if(dataStorage != null)
        {
            dataStorage.DistributeRemainingCapacity("wood", Mathf.FloorToInt(_woodSpent * returnPercentage));
            dataStorage.DistributeRemainingCapacity("stone", Mathf.FloorToInt(_stoneSpent * returnPercentage));
            dataStorage.DistributeRemainingCapacity("metal", Mathf.FloorToInt(_metalSpent * returnPercentage));
            dataStorage.DistributeRemainingCapacity("food", Mathf.FloorToInt(_foodSpent * returnPercentage));
            dataStorage.DistributeRemainingCapacity("water", Mathf.FloorToInt(_waterSpent * returnPercentage));
        }
    }
}
