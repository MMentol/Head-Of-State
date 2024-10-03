using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class StoneCheckRandom : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    public HumanStats _hStats;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public StoneCheckRandom(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        this._hStats = transform.GetComponent<HumanStats>();

    }

    public override NodeState Evaluate()
    {
        var randMat = (int)GetData("random");


        if (randMat == 2)
        {
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
