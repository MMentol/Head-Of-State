using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class checkAgeTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    public HumanStats _hStats;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public checkAgeTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        this._hStats = transform.GetComponent<HumanStats>();

    }

    public override NodeState Evaluate()
    {
        if (_hStats.inBabyPhase == 0)
            state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
