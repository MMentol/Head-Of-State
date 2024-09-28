using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] public GameObject ocUI;
    GameObject realUI;
    [SerializeField] TMP_Text StructNameTxt;
    [SerializeField] TMP_Text ResidentsTxt;
    [SerializeField] TMP_Text StorageTxt;

    void Start(){
        this.ocUI = GameObject.FindWithTag("BuildingUI");
        realUI = ocUI.transform.GetChild(9).GetChild(0).gameObject;
        StructNameTxt = realUI.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        ResidentsTxt = realUI.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        StorageTxt = realUI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
    }
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
                    ocUI.transform.GetChild(9).gameObject.SetActive(true);
                    StructNameTxt.text = name;
                    ResidentsTxt.text = "Residents: ";
                    StorageTxt.text = "Storage: ";
                    Debug.Log("Structure Script UI Children: " + ocUI.transform.childCount);
                }
            }
        }
    }
}
