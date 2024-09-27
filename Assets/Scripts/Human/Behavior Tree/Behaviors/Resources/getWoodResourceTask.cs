using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using BehaviorTree;
using GridMap.Resources;


public class getWoodResourceTask : Node
{
    private HumanController humanController;
    private Transform _transform;
    private HumanStats _hStats;
    public Inventory inventory;


    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public getWoodResourceTask(Transform transform, Inventory inve )
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        this.inventory = inve;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("wood");

        if (_transform.position.Equals(target.position))
        {
            //add food to human
            //remove food from tile
            GameObject woodTile = (GameObject)GetData("wood");
            TreeResource wood = woodTile.GetComponent<TreeResource>();

            int harvested = wood.Harvest(1);
            string resource = "wood";

            if (harvested > 0)
                this.inventory.AddToInventory(resource, harvested);



            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
