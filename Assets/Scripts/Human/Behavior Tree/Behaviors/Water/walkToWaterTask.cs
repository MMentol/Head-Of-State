using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;

using BehaviorTree;

public class walkToWaterTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToWaterTask(Transform transform)
    {
        rootTree = transform.GetComponent<HumanBT>();
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {
        WaterResource target = (WaterResource)GetData("water");

        if (Vector3.Distance(_transform.position, target.transform.position) > 0.1f)
        {
            humanController.SetTargetPosition(target.transform.position);

        }
        rootTree.currentAction = "walkToWater";
        state = NodeState.RUNNING;
        return state;
    }
}
