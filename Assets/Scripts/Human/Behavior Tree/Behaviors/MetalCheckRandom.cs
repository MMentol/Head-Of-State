using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MetalCheckRandom : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    public HumanStats _hStats;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public MetalCheckRandom(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        this._hStats = transform.GetComponent<HumanStats>();

    }

    public override NodeState Evaluate()
    {
        //parent.parent.SetData("random", -1);
        var randMat = (int)GetData("random");
        
        if (randMat == -1)
        {
            return NodeState.FAILURE;
        }


        if (randMat == 1)
        {
            Debug.Log("rad?L " + randMat);

            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
