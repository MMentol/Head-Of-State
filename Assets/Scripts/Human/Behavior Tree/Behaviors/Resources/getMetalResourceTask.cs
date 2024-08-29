using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class getMetalResourceTask : Node
{
    private HumanController humanController;
    private Transform _transform;
    private HumanStats _hStats;

    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public getMetalResourceTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _hStats = transform.GetComponent<HumanStats>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("metal");

        if (_transform.position.Equals(target.position))
        {
            //add food to human
            //remove food from tile
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
