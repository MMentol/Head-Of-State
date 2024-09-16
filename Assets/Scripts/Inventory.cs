using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [Header("Inventory Settings")]
    public bool logDebug = false;
    [SerializeField] private int size = 100;
    [SerializeField] public InventoryList initList = new InventoryList();
    [SerializeField] public Dictionary<string, int> _resources = new Dictionary<string, int>();
    
    public void Init(string type, int resourceAmount, bool debug = false)
    {
        logDebug = debug;
        if(logDebug){Debug.Log($"Inv Init.");}
        initList.AddItemType(type, resourceAmount);
        _resources = initList.ToDictionary();
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
            ResourceNode node = gameObject.GetComponent<ResourceNode>();
            if(node != null)
            {
                gameObject.GetComponent<Structure>()._tile.DestroyStructure();
            }
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
        if(_resources.ContainsKey(type))
        {
            _resources[type] = count;
        }
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

[Serializable]
public class InventoryList
{
    [SerializeField] List<InventoryItem> inventoryItems;

    public InventoryList()
    {
        inventoryItems = new List<InventoryItem>();
    }

    public Dictionary<string, int> ToDictionary()
    {
        Dictionary<string, int> list = new Dictionary<string, int>();
        foreach (var item in inventoryItems)
        {
            list.Add(item.name, item.amount);
        }
        return list;
    }

    public void AddItemType(string type, int amount)
    {
        // Check if the item already exists
        if (inventoryItems.Any(item => item.name == type))
        {
            // Handle duplicates if needed
            Debug.LogWarning("Item already exists: " + type);
            return;
        }

        // Add the new item to the list
        inventoryItems.Add(new InventoryItem(type, amount));
    }
}

[Serializable]
public class InventoryItem
{
    [SerializeField] public string name;
    [SerializeField] public int amount;

    public InventoryItem(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    }

}