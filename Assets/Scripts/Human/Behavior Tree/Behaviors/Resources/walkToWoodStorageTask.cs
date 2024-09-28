using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Structures.Storage;

using BehaviorTree;

public class walkToWoodStorageTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToWoodStorageTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {

        WoodStorage storage = (WoodStorage)GetData("woodStorage");
        if (storage == null)
            return NodeState.FAILURE;
        if (Vector3.Distance(_transform.position, storage.transform.position) > 0.1f)
        {

            humanController.SetTargetPosition(storage.transform.position);

        }
        state = NodeState.RUNNING;
        Debug.Log("statewalks :" + state);


        return state;
    }
}
