using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Structure : MonoBehaviour
{
    [Header("Structure Properties")]
    [SerializeField] public string name = "Structure";
    [SerializeField] public bool isPlaced = false;
    [SerializeField] public bool isRemovable = true;
    [SerializeField] public Vector2 _currentPos;
    [SerializeField] public Tile _tile;

    [Header("Resource Placement Costs")]
    [SerializeField] public float _woodCost = 0;
    [SerializeField] public float _stoneCost = 0;
    [SerializeField] public float _metalCost = 0;
    [SerializeField] public float _waterCost = 0;

    [Header("On-Click UI")]
    [SerializeField] public GameObject ocUI;
    GameObject realUI;
    [SerializeField] TMP_Text BuildUIBox;

    void Start(){
        this.ocUI = GameObject.FindWithTag("BuildingUI");
        realUI = ocUI.transform.GetChild(7).gameObject;
        BuildUIBox = realUI.GetComponent<TMP_Text>();
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
                    ocUI.transform.GetChild(7).gameObject.SetActive(true);
                    BuildUIBox.text = "Structure: " + name + "\nResidents: " + "\nStorage: ";
                    Debug.Log("Structure Script UI Children: " + ocUI.transform.childCount);
                }
            }
        }
    }
}
