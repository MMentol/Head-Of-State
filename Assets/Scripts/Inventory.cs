using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Demos.Complex;

namespace Scripts
{
    public class Inventory : MonoBehaviour {
        [Header("Inventory Settings")]
        public bool logDebug = false;
        [SerializeField] public int size = 100;
        [SerializeField] private int used = 100;
        //[SerializeField] public InventoryList initList = new InventoryList();
        //[SerializeField] public Dictionary<string, int> _resources = new Dictionary<string, int>();

        [SerializeField] public Dictionary<string, int> _resources = new Dictionary<string, int>
        {
            {"wood", 0}, {"metal", 0}, {"stone", 0}, {"food", 0}, {"water", 0},
        };

        public void Init(string type, int resourceAmount, bool debug = false)
        {
            logDebug = debug;
            if(logDebug){Debug.Log($"Inv Init.");}
            //initList.AddItemType(type.ToLower(), resourceAmount);
            //_resources = initList.ToDictionary();
            size = resourceAmount;
            used = resourceAmount;
            SetResource(type, resourceAmount);
        }

        public int GetRemainingCapacity()
        {
            int used = _resources.Values.Sum();
            this.used = used;
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
            //Debug.Log(_resources[resource.ToLower()]);
            //float percentage = (float) _resources[resource.ToLower()] / (float) this.size;
            //Debug.Log(percentage);
            return _resources[resource.ToLower()];
        }
        public int GetResource(string type, int count)
        {
            type = type.ToLower();
            int stored = _resources[type];
            if (count > stored)
            {
                int remaining = count - stored;
                _resources[type] = 0;
                return remaining;
            }
            else
            {
                _resources[type] -= count;
            }
            
            return count;
        }
        public int AddResource(string type, int count)
        {
            int capacity = GetRemainingCapacity();
            //If to be added < capacity
            if(count <= capacity)
            {
                //to add = count
                int added = count;
                _resources[type.ToLower()] += added;
                //remaining
                return 0;
            }

            return count - capacity;
        }
        public void SetResource(string type, int count)
        {
            if(_resources.ContainsKey(type))
            {
                _resources[type.ToLower()] = count;
            }
        }
        //For testing  
        void OnMouseOver() {
            if(Input.GetMouseButtonUp(0) && logDebug)
            {
                //Debug.Log($"Available: {GetRemainingCapacity()}");
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
            this.name = name.ToLower();
            this.amount = amount;
        }

    }
}