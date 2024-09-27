using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class checkFoodTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    public HumanStats _hStats;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public checkFoodTask(Transform transform, HumanStats hstat)
    {
        _animator = transform.GetComponent<Animator>();
        this._hStats = hstat;

    }

    public override NodeState Evaluate()
    {
        if (_hStats._hunger > 80)
            state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
