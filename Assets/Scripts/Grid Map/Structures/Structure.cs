using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
