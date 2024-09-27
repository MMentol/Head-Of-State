using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class walkToMetalStorageTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToMetalStorageTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("metalStorage");

        if (Vector3.Distance(_transform.position, target.position) > 0.1f)
        {
            humanController.SetTargetPosition(target.position);

        }
        state = NodeState.RUNNING;
        return state;
    }
}
