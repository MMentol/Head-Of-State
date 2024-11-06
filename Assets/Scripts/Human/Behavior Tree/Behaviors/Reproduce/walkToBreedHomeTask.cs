using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class walkToBreedHomeTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToBreedHomeTask(Transform transform)
    {
        _transform = transform;
        rootTree = transform.GetComponent<HumanBT>();
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {
        House target = (House)GetData("bhome");
        //if(target == null)
        //{
        //    return NodeState.FAILURE;
        //}

        if (Vector3.Distance(_transform.position, target.transform.position) > 0.1f)
        {
            humanController.SetTargetPosition(target.transform.position);

        }
        rootTree.currentAction = "walkToHomeToReproduce";
        state = NodeState.RUNNING;
        return state;
    }
}
