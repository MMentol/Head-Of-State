using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class checkMetalResourceTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;


    private MaterialDataStorage metalAmount;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public checkMetalResourceTask(Transform transform, MaterialDataStorage storage)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        metalAmount = storage;
    }

    public override NodeState Evaluate()
    {
        if (metalAmount == null)
            state = NodeState.FAILURE;



        if (((float)this.metalAmount.Metal / (float)this.metalAmount.MetalCapacity)! >= 100)
            state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
