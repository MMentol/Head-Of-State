using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using BehaviorTree;
using GridMap.Resources;


public class getFoodTask : Node
{
    private HumanController humanController;
    private Transform _transform;
    private HumanStats _hStats;
    public Inventory inventory;


    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public getFoodTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        inventory = transform.GetComponent<Inventory>();
    }

    public override NodeState Evaluate()
    {
        GameObject foodTile = (GameObject)GetData("food");

        if (foodTile == null) return NodeState.FAILURE;

        if (this.inventory.used == this.inventory.size)
        {
            state = NodeState.SUCCESS;
            Debug.Log("stateget1 :" + state);

            return state;
        }
        if (_transform.position.Equals(foodTile.transform.position))
        {
            //add food to human
            //remove food from tile
            FoodResource food = foodTile.GetComponent<FoodResource>();

            int harvested = food.Harvest(1);
            string resource = "food";

            //if (harvested > 0)
            //    this.inventory.addtoinventory(resource, harvested);

            _hStats._hunger -= 25;

            state = NodeState.SUCCESS;
            Debug.Log("stateget :" + state);
            ClearData("food");
            return state;
        }
        state = NodeState.FAILURE;
        Debug.Log("stateget :" + state);

        return state;
    }
}
