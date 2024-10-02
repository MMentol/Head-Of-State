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
    private StoneStorage[] stoneStorage;

    public checkForStoneStorageTask(Transform transform)
    {
        this._animator = transform.GetComponent<Animator>();
        this._transform = transform;
    }


    public override NodeState Evaluate()
    {
        this.stoneStorage = GameObject.FindObjectsOfType<StoneStorage>();

        Debug.Log("checking: " + this.stoneStorage + " " + stoneStorage.Length);

        if (stoneStorage == null || stoneStorage.Length < 1)
        {
            state = NodeState.FAILURE;
            Debug.Log("nostone");
            return state;
        }
        var closest = this.stoneStorage
            .Where(x => x.Capacity > x.Count)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();

        if (closest == null)
        {
            state = NodeState.FAILURE;
            Debug.Log("checking stone: " + state );

            return state;
        }
        else
            while (closest.Capacity <= closest.Count)
            {
                var list = this.stoneStorage.ToList();
                list.Remove(closest);
                stoneStorage = list.ToArray();
                closest = this.stoneStorage
                .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                .FirstOrDefault();
                if (closest == null)
                    return NodeState.FAILURE;
            }

        parent.parent.SetData("stoneStorage", closest);

        state = NodeState.SUCCESS;

        return state;
    }
}
