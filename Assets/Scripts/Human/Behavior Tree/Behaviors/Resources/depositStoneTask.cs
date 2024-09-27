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

    public depositStoneTask(Transform transform, Inventory inve)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        inventory = inve;

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("stoneStorage");

        if (_transform.position.Equals(target.position))
        {
            //add food to human
            //remove food from tile
            GameObject stone = (GameObject)GetData("stoneStorage");
            StoneStorage stoneStorage = stone.GetComponent<StoneStorage>();

            string resource = "stone";
            int withdraw = stoneStorage.Add(inventory.GetResourceCount(resource));
            inventory.GetFromInventory(resource, withdraw);



            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
