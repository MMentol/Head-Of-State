using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start(){
        this.ocUI = GameObject.FindWithTag("BuildingUI");
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
                this.ocUI.SetActive(true);
            }
        }
    }
}
