using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using BehaviorTree;
using GridMap.Resources;


public class getStoneResourceTask : Node
{
    private HumanController humanController;
    private Transform _transform;
    private HumanStats _hStats;
    public Inventory inventory;


    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public getStoneResourceTask(Transform transform, Inventory inve)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        inventory = inve;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("stone");

        if (_transform.position.Equals(target.position))
        {
            //add food to human
            //remove food from tile
            GameObject stoneTile = (GameObject)GetData("stone");
            StoneResource stone = stoneTile.GetComponent<StoneResource>();

            int harvested = stone.Harvest(1);
            string resource = "stone";

            if (harvested > 0)
                inventory.AddToInventory(resource, harvested);



            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
