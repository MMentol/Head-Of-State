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

    private float timer = 0f;
    private float delay = 0.5f;

    public getWoodResourceTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        rootTree = transform.GetComponent<HumanBT>();
        _hStats = transform.GetComponent<HumanStats>();
        this.inventory = transform.GetComponent<Inventory>();
    }

    public override NodeState Evaluate()
    {
        TreeResource woodTile = (TreeResource)GetData("wood");

        if (woodTile == null) return NodeState.FAILURE;

        if(this.inventory.inventoryContents.GetValueOrDefault("wood") > 0)
        {
            state = NodeState.SUCCESS;
            Debug.Log("stateget1 :" + state);

            return state;
        }
        if (_transform.position.Equals(woodTile.transform.position))
        {
            rootTree.currentAction = "getWood";
            //add food to human
            //remove food from tile
            //TreeResource wood = woodTile.GetComponent<TreeResource>();            
            
            if (timer < delay)
            {
                timer += Time.deltaTime;
                return NodeState.RUNNING;
            }

            timer = 0f;
            int harvested = woodTile.Harvest(1);
            string resource = "wood";

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
