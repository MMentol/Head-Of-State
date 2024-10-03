using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using GridMap.Structures.Storage;

using BehaviorTree;

public class depositStoneTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;
    public Inventory inventory;


    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public depositStoneTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        this.inventory = transform.GetComponent<Inventory>();

    }

    public override NodeState Evaluate()
    {
        StoneStorage stone = (StoneStorage)GetData("stoneStorage");

        if (stone == null || inventory.inventoryContents.GetValueOrDefault("stone") == 0 || ((float)stone.Count / (float)stone.Capacity) >= 1)
            return NodeState.FAILURE;

       // Debug.Log("Capacity: " + ((float)stone.Count / (float)stone.Capacity));
        if (_transform.position.Equals(stone.transform.position))
        {
            //add food to human
            //remove food from tile
            StoneStorage stoneStorage = stone.GetComponent<StoneStorage>();

            ///Debug.Log("Inventory of:" + this.inventory);
            string resource = "stone";
            int withdraw = stoneStorage.Add(inventory.GetResourceCount(resource));
           /// Debug.Log("withdraw: " + withdraw);
            int taken = this.inventory.GetFromInventory(resource, withdraw);
            ///Debug.Log("Taken:" + taken);


            state = NodeState.SUCCESS;
            if (inventory.inventoryContents.GetValueOrDefault("stone") == 0)
            {
                ClearData("stoneStorage");
                ClearData("stone");
                ClearData("random");
            }

           // Debug.Log("stateDep :" + state);

            return state;
        }
        state = NodeState.FAILURE;
       // Debug.Log("stateDep :" + state);

        return state;
    }
}
