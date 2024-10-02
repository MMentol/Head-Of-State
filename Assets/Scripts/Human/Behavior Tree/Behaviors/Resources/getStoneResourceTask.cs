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

    public getStoneResourceTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        this.inventory = transform.GetComponent<Inventory>();
    }

    public override NodeState Evaluate()
    {
        StoneResource stoneTile = (StoneResource)GetData("stone");

        if (stoneTile == null) return NodeState.FAILURE;

        if (this.inventory.inventoryContents.GetValueOrDefault("stone") > 0)
        {
            state = NodeState.SUCCESS;
            Debug.Log("stateget1 :" + state);

            return state;
        }
        if (_transform.position.Equals(stoneTile.transform.position))
        {
            //add food to human
            //remove food from tile
            //TreeResource stone = stoneTile.GetComponent<TreeResource>();

            int harvested = stoneTile.Harvest(1);
            string resource = "stone";

            if (harvested > 0)
                this.inventory.AddToInventory(resource, harvested);



            state = NodeState.SUCCESS;
            Debug.Log("stateget :" + state);

            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log("stateget :" + state);

        return state;
    }
}
