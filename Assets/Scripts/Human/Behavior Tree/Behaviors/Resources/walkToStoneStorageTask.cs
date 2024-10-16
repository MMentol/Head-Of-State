using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Structures.Storage;

using BehaviorTree;

public class walkToStoneStorageTask : Node
{
    private HumanController humanController;
    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public walkToStoneStorageTask(Transform transform)
    {
        _transform = transform;
        rootTree = transform.GetComponent<HumanBT>();
        humanController = transform.GetComponent<HumanController>();
    }

    public override NodeState Evaluate()
    {

        StoneStorage storage = (StoneStorage)GetData("stoneStorage");
        if (storage == null)
            return NodeState.FAILURE;
        if (Vector3.Distance(_transform.position, storage.transform.position) > 0.1f)
        {

            humanController.SetTargetPosition(storage.transform.position);
            //Debug.Log("storage stone  pos : " + storage.transform.position);
        }
        rootTree.currentAction = "walkToStoneStorage";
        state = NodeState.RUNNING;
        ////Debug.Log("statewalks :" + state);


        return state;
    }
}
