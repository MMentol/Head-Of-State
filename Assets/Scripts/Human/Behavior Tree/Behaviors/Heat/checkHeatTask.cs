using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class checkHeatTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public checkHeatTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        
        return state;
    }
}
