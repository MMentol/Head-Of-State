using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Demos.Complex;

public class Inventory : MonoBehaviour {
    [Header("Inventory Settings")]
    public bool logDebug = false;
    [SerializeField] private int size = 100;
    [SerializeField] public Dictionary<string, int> _resources = new Dictionary<string, int>{
        { "wood", 0 },
        { "metal", 0 },
        { "stone", 0 },
        { "water", 0 },
        { "food", 0 }
    };
    
    public void Init(string type, int resourceAmount, bool debug = false)
    {
        logDebug = debug;
        size = resourceAmount;
        SetResource(type, resourceAmount);
    }

    public int GetRemainingCapacity()
    {
        int used = _resources.Values.Sum();
        if (used > size)
        {
            Debug.LogError($"{gameObject.name} Used storage is more than capacity.");
            return 0;
        }
        return size - used;
    }


    //Resource functions
    public int GetResourceCount(string resource)
    {
        return _resources[resource];
    }
    public int GetResource(string type, int count)
    {
        int stored = _resources[type];
        if (count >= stored)
        {
            stored = 0;
            return count;
        }
        else
        {
            stored -= count;
            return count;
        }
        if(_resources.Values.Sum() == 0)
        {
            gameObject.GetComponent<Structure>()._tile.DestroyStructure();
        }
    }
    public int AddResource(string type, int count)
    {
        int capacity = GetRemainingCapacity();
        if(count <= capacity)
        {
            int added = count - capacity;
            _resources[type] += added;
            return count - added;
        }

        return count;
    }
    public void SetResource(string type, int count)
    {
        _resources[type] = count;
    }
    //For testing  
    void OnMouseOver() {
        if(Input.GetMouseButtonUp(0) && logDebug)
        {
            Debug.Log($"Available: {GetRemainingCapacity()}");
            foreach (var resource in _resources)
            {
                Debug.Log($"{resource.Key}: {resource.Value}");
            }
        }
    }
}