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

    private float timer = 0f;
    private float delay = 0.5f;

    public getWaterTask(Transform transform)
    {
        rootTree = transform.GetComponent<HumanBT>();
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
        inventory = transform.GetComponent<Inventory>();
    }

    public override NodeState Evaluate()
    {
        WaterResource waterTile = (WaterResource)GetData("water");

        if (waterTile == null) return NodeState.FAILURE;

        //if (this.inventory.used == this.inventory.size)
        //{
        //    state = NodeState.SUCCESS;
        //    Debug.Log("stateget1 :" + state);

        //    return state;
        //}
        if (Vector3.Distance(_transform.position, waterTile.transform.position)<=2f && _hStats._thirst>=80)
        {
            rootTree.currentAction = "getWater";
            
            //add water to human
            //remove water from tile
            WaterResource water = waterTile.GetComponent<WaterResource>();

            if (timer < delay)
            {
                timer += Time.deltaTime;
                Debug.Log($"Delay {timer}");
                return NodeState.RUNNING;
            }

            timer = 0f;

            int harvested = water.Harvest(1);
            string resource = "water";

            //if (harvested > 0)
            //    this.inventory.addtoinventory(resource, harvested);

            _hStats._thirst -= 80;

            state = NodeState.SUCCESS;
            //Debug.Log("stateget :" + state);
            ClearData("water");
            return state;
        }
        state = NodeState.FAILURE;
        //Debug.Log("stateget :" + state);

        return state;
    }
}
