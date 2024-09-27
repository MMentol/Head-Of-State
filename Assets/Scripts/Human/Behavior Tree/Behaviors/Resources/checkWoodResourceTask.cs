using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class checkWoodResourceTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    private MaterialDataStorage woodAmount;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public checkWoodResourceTask(Transform transform, MaterialDataStorage  storage)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        woodAmount = storage;
    }

    public override NodeState Evaluate()
    {
        if (woodAmount == null)
            state = NodeState.FAILURE;



        if (((float)this.woodAmount.Wood / (float)this.woodAmount.WoodCapacity)! >= 100)
            state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
