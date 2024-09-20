using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Scripts
{
    public class Inventory : MonoBehaviour {
        [Header("Inventory Settings")]
        public bool logDebug = false;
        public int size = 100;
        public int used = 0;
        public Dictionary<string, int> _resources = new Dictionary<string, int>
        {
            {"wood", 0}, {"metal", 0}, {"stone", 0}, {"food", 0}, {"water", 0},
        };

        //Getting Current ItemCount / Capacity
        public int GetResourceCount(string resource)
        {
            return _resources[resource.ToLower()];
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

        //Storage Functions        
        public int GetFromInventory(string type, int toWithdraw)
        {
            type = type.ToLower();
            int stored = _resources[type];
            int withdrawn = Math.Min(toWithdraw, stored);
            _resources[type] -= withdrawn;
            return withdrawn;
        }
        public int AddToInventory(string type, int toDeposit)
        {
            type = type.ToLower();
            int remainingCapacity = GetRemainingCapacity();
            int deposited = Mathf.Min(toDeposit, remainingCapacity);
            _resources[type] += deposited;
            return toDeposit - deposited;
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
}