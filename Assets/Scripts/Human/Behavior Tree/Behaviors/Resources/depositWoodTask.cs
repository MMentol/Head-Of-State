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

    public depositWoodTask(Transform transform, Inventory inve)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        inventory = inve;

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("woodStorage");

        if (_transform.position.Equals(target.position))
        {
            //add food to human
            //remove food from tile
            GameObject wood = (GameObject)GetData("woodStorage");
            WoodStorage woodStorage = wood.GetComponent<WoodStorage>();

            string resource = "wood";
            int withdraw = woodStorage.Add(inventory.GetResourceCount(resource));
            inventory.GetFromInventory(resource, withdraw);



            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
