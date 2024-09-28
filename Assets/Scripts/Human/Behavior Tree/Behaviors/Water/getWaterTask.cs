using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using BehaviorTree;
using GridMap.Resources;


public class getWaterTask : Node
{
    private HumanController humanController;
    private Transform _transform;
    private HumanStats _hStats;
    public Inventory inventory;


    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public getWaterTask(Transform transform, Inventory inve)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        inventory = inve;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("water");

        if (_transform.position.Equals(target.position))
        {
            //add water to human
            //remove water from tile
            GameObject waterTile = (GameObject)GetData("water");
            WaterResource water = waterTile.GetComponent<WaterResource>();

            int harvested = water.Harvest(1);
            string resource = "water";

            if (harvested > 0)
                inventory.AddToInventory(resource, harvested);



            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
