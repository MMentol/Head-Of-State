using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using BehaviorTree;
using GridMap.Resources;


public class getMetalResourceTask : Node
{
    private HumanController humanController;
    private Transform _transform;
    private HumanStats _hStats;
    public Inventory inventory;


    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public getMetalResourceTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        this.inventory = transform.GetComponent<Inventory>();
    }

    public override NodeState Evaluate()
    {
        MetalResource metalTile = (MetalResource)GetData("metal");

        if (metalTile == null) return NodeState.FAILURE;

        if (this.inventory.used == this.inventory.size)
        {
            state = NodeState.SUCCESS;
            Debug.Log("stateget1 :" + state);

            return state;
        }
        if (_transform.position.Equals(metalTile.transform.position))
        {
            //add food to human
            //remove food from tile
            //TreeResource metal = metalTile.GetComponent<TreeResource>();

            int harvested = metalTile.Harvest(1);
            string resource = "metal";

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
