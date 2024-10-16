using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using GridMap.Structures.Storage;

using BehaviorTree;

public class depositWoodTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;
    public Inventory inventory;


    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public depositWoodTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        rootTree = transform.GetComponent<HumanBT>();
        this.inventory = transform.GetComponent<Inventory>();

    }

    public override NodeState Evaluate()
    {
        WoodStorage wood = (WoodStorage)GetData("woodStorage");
        
        if (wood == null || inventory.inventoryContents.GetValueOrDefault("wood")== 0 || ((float)wood.Count/(float)wood.Capacity)>=1)
            return NodeState.FAILURE;

        Debug.Log("Capacity: " + ((float)wood.Count / (float)wood.Capacity));
        if (_transform.position.Equals(wood.transform.position))
        {
            rootTree.currentAction = "depositWood";
            //add food to human
            //remove food from tile
            WoodStorage woodStorage = wood.GetComponent<WoodStorage>();

            Debug.Log("Inventory of:" + this.inventory);
            string resource = "wood";
            int withdraw = woodStorage.Add(inventory.GetResourceCount(resource));
            Debug.Log("withdraw: " + withdraw);
            int taken  = this.inventory.GetFromInventory(resource, withdraw);
            Debug.Log("Taken:" + taken);


            state = NodeState.SUCCESS;
            if (inventory.inventoryContents.GetValueOrDefault("wood") == 0)
            {
                ClearData("woodStorage");
                ClearData("wood");
                ClearData("random");
            }
                    
            Debug.Log("stateDep :" + state);

            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log("stateDep :" + state);

        return state;
    }
}
