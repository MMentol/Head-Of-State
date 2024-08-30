using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [Header("Inventory Settings")]
    [SerializeField] private int size = 100;
    [SerializeField] private Dictionary<string, int> _resources = new Dictionary<string, int>{
        { "wood", 10 },
        { "metal", 20 },
        { "stone", 30 },
        { "water", 40 },
        { "food", 50 }
    };
    
    public int GetRemainingCapacity()
    {
        int used = _resources.Values.Sum();
        if (used >= size)
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

    //For testing  
    void OnMouseOver() {
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log($"Available: {GetRemainingCapacity()}");
            foreach (var resource in _resources)
            {
                Debug.Log($"{resource.Key}: {resource.Value}");
            }
        }
    }
}