using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using GridMap.Structures.Storage;

using BehaviorTree;

public class depositMetalTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;
    public Inventory inventory;


    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public depositMetalTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        rootTree = transform.GetComponent<HumanBT>();
        this.inventory = transform.GetComponent<Inventory>();

    }

    public override NodeState Evaluate()
    {
        MetalStorage metal = (MetalStorage)GetData("metalStorage");

        if (metal == null || inventory.inventoryContents.GetValueOrDefault("metal") == 0 || ((float)metal.Count / (float)metal.Capacity) >= 1)
            return NodeState.FAILURE;

        ///Debug.Log("Capacity: " + ((float)metal.Count / (float)metal.Capacity));
        if (_transform.position.Equals(metal.transform.position))
        {
            rootTree.currentAction = "depositMetal";
            //add food to human
            //remove food from tile
            MetalStorage metalStorage = metal.GetComponent<MetalStorage>();

            //Debug.Log("Inventory of:" + this.inventory);
            string resource = "metal";
            int withdraw = metalStorage.Add(inventory.GetResourceCount(resource));
            //Debug.Log("withdraw: " + withdraw);
            int taken = this.inventory.GetFromInventory(resource, withdraw);
            //Debug.Log("Taken:" + taken);


            state = NodeState.SUCCESS;
            if (inventory.inventoryContents.GetValueOrDefault("metal") == 0)
            {
                ClearData("metalStorage");
                ClearData("metal");
                ClearData("random");

                parent.parent.parent.SetData("random", -1);
                //ClearData("random");
            }

            //Debug.Log("stateDep :" + state);

            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log("stateDep :" + state);

        return state;
    }
}
