using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;

using BehaviorTree;

public class walkToFoodTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToFoodTask(Transform transform)
    {
        rootTree = transform.GetComponent<HumanBT>();
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {
        FoodResource target = (FoodResource)GetData("food");

        if (target == null)
        {
            ClearData("food");
            return NodeState.FAILURE;
        }

        if (Vector3.Distance(_transform.position, target.transform.position) > 0.1f)
        {
            humanController.SetTargetPosition(target.transform.position);
            //Debug.Log("statewalk : YES " + target.transform.position);
        }
        rootTree.currentAction = "walkToFood";
        state = NodeState.RUNNING;
        return state;
    }
}
