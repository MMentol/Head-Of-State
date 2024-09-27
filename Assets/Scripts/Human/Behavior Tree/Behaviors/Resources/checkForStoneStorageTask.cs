using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BehaviorTree;
using GridMap.Structures.Storage;

public class checkForStoneStorageTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private GameObject[] stoneStorage;

    public checkForStoneStorageTask(Transform transform, GameObject[] storage)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        stoneStorage = storage;
    }

    public override NodeState Evaluate()
    {
        if (stoneStorage == null)
            state = NodeState.FAILURE;

        var closest = this.stoneStorage
            //.Where(x => x.Capacity > x.Count)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
        var close = closest.GetComponent<StoneStorage>();
        if (close == null)
            return NodeState.FAILURE;
        else
            while (close.Capacity <= close.Count)
            {
                var list = this.stoneStorage.ToList();
                list.Remove(closest);
                stoneStorage = list.ToArray();
                closest = this.stoneStorage
                .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                .FirstOrDefault();
                close = closest.GetComponent<StoneStorage>();
                if (close == null)
                    return NodeState.FAILURE;
            }

        parent.parent.SetData("stoneStorage", closest);
        state = NodeState.SUCCESS;

        return state;
    }
}
