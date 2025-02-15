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

    private float timer = 0f;
    private float delay = 0.5f;

    public getFoodTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        inventory = transform.GetComponent<Inventory>();
        rootTree = transform.GetComponent<HumanBT>();

    }

    public override NodeState Evaluate()
    {
        FoodResource foodTile = (FoodResource)GetData("food");

        if (foodTile == null) return NodeState.FAILURE;

        //Debug.Log("food: " + _transform.position);

        if (_transform.position.Equals(foodTile.transform.position)&& _hStats._hunger>=20)
        {
            rootTree.currentAction = "getFood";
            //add food to human
            //remove food from tile
            FoodResource food = foodTile.GetComponent<FoodResource>();

            if (timer < delay)
            {
                timer += Time.deltaTime;
                Debug.Log($"Delay {timer}");
                return NodeState.RUNNING;
            }

            timer = 0f;

            int harvested = food.Harvest(1);
            string resource = "food";

            //if (harvested > 0)
            //    this.inventory.addtoinventory(resource, harvested);

            _hStats._hunger -= 80;

            state = NodeState.SUCCESS;
            //Debug.Log("stateget :" + state);
            ClearData("food");
            return state;
        }
        state = NodeState.FAILURE;
        //Debug.Log("stateget :" + state);

        return state;
    }
}
