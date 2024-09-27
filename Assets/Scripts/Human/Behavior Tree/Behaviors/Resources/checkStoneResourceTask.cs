using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class checkStoneResourceTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    private MaterialDataStorage stoneAmount;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public checkStoneResourceTask(Transform transform, MaterialDataStorage storage)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        stoneAmount = storage;
    }

    public override NodeState Evaluate()
    {
        if (stoneAmount == null)
            state = NodeState.FAILURE;



        if (((float)this.stoneAmount.Stone / (float)this.stoneAmount.StoneCapacity)! >= 100)
            state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
