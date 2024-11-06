using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;

using BehaviorTree;

public class walkToMetalResourceTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToMetalResourceTask(Transform transform)
    {
        rootTree = transform.GetComponent<HumanBT>();
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {
        MetalResource target = (MetalResource)GetData("metal");

        if (target == null)
        {
            ClearData("metal");
            return NodeState.FAILURE;
        }

        if (Vector3.Distance(_transform.position, target.transform.position) > 0.1f)
        {
            humanController.SetTargetPosition(target.transform.position);
            Debug.Log("statewalk : YES " + target.transform.position);

        }
        rootTree.currentAction = "walkToMetalResource";
        state = NodeState.RUNNING;
        return state;
    }
}
