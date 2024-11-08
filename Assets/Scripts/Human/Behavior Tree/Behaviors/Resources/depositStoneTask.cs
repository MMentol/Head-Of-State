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

    private float timer = 0f;
    private float delay = 0.3f; //3 seconds~ish

    public depositStoneTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        rootTree = transform.GetComponent<HumanBT>();
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
            rootTree.currentAction = "depositStone";
            //add food to human
            //remove food from tile
            StoneStorage stoneStorage = stone.GetComponent<StoneStorage>();

            if (timer < delay)
            {
                timer += Time.deltaTime;
                return NodeState.RUNNING;
            }

            timer = 0f;

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

                parent.parent.parent.SetData("random", -1);
                //ClearData("random");
            }

           // Debug.Log("stateDep :" + state);

            return state;
        }
        state = NodeState.FAILURE;
       // Debug.Log("stateDep :" + state);

        return state;
    }
}
