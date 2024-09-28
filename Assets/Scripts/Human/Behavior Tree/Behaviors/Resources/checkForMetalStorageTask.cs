using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BehaviorTree;
using GridMap.Structures.Storage;

public class checkForMetalStorageTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private MetalStorage[] metalStorage;

    public checkForMetalStorageTask(Transform transform)
    {
        this._animator = transform.GetComponent<Animator>();
        this._transform = transform;
    }


    public override NodeState Evaluate()
    {
        this.metalStorage = GameObject.FindObjectsOfType<MetalStorage>();

        Debug.Log("checking: " + this.metalStorage);

        if (metalStorage.Length == 0)
        {
            state = NodeState.FAILURE;

            return state;
        }
        var closest = this.metalStorage
            .Where(x => x.Capacity > x.Count)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
        var close = closest.GetComponent<MetalStorage>();

        if (close == null)
        {
            state = NodeState.FAILURE;
            Debug.Log("checking: " + state);

            return state;
        }
        else
            while (close.Capacity <= close.Count)
            {
                var list = this.metalStorage.ToList();
                list.Remove(closest);
                metalStorage = list.ToArray();
                closest = this.metalStorage
                .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                .FirstOrDefault();
                close = closest.GetComponent<MetalStorage>();
                if (close == null)
                    return NodeState.FAILURE;
            }

        parent.parent.SetData("metalStorage", closest);

        state = NodeState.SUCCESS;

        return state;
    }
}
