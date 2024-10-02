using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GridMap.Structures.Storage;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    [Header("Structure Properties")]
    public string name = "Structure Name";
    public bool isPlaced = false;
    public bool isRemovable = true;
    public Vector2 _currentPos;
    public Tile _tile;

    [Header("Resource Placement Costs")]
    public int _woodCost = 0;
    public int _stoneCost = 0;
    public int _metalCost = 0;
    public int _foodCost = 0;
    public int _waterCost = 0;

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
            if(Input.GetMouseButtonUp(1))
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
}
