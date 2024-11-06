using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;

using BehaviorTree;

public class walkToWoodResourceTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToWoodResourceTask(Transform transform)
    {
        _transform = transform;
        rootTree = transform.GetComponent<HumanBT>();
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {
        TreeResource target = (TreeResource)GetData("wood");

        if(target == null)
        {
            ClearData("wood");
            return NodeState.FAILURE;
        }

        if (Vector3.Distance(_transform.position, target.transform.position) > 0.1f)
        {
            humanController.SetTargetPosition(target.transform.position);
            Debug.Log("statewalk : YES "+ target.transform.position);

        }
        rootTree.currentAction = "walkToWoodResource";
        state = NodeState.RUNNING;
        return state;
    }
}
