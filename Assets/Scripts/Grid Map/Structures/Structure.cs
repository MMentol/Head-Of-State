using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    [Header("Structure Properties")]
    [SerializeField] public bool isPlaced = false;
    [SerializeField] public Vector2 _currentPos;
    [SerializeField] public Tile _tile;

    /*[Header("Resource Placement Costs")]
    [SerializeField] private float _woodCost = 0;
    [SerializeField] private float _stoneCost = 0;
    [SerializeField] private float _metalCost = 0;
    [SerializeField] private float _waterCost = 0;*/

    void OnMouseOver() {
        if(isPlaced)
        {
            if(Input.GetMouseButtonUp(1))
            {
                Debug.Log("Rightclick structure");
                _tile.DestroyStructure();
            }
        }
    }
}
