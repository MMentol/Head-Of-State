using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Structures.Storage;

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
        rootTree = transform.GetComponent<HumanBT>();
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {

        MetalStorage storage = (MetalStorage)GetData("metalStorage");
        if (storage == null)
            return NodeState.FAILURE;
        if (Vector3.Distance(_transform.position, storage.transform.position) > 0.1f)
        {

            humanController.SetTargetPosition(storage.transform.position);

        }
        rootTree.currentAction = "walkToMetalStorage";
        state = NodeState.RUNNING;
        Debug.Log("statewalks :" + state);


        return state;
    }
}
