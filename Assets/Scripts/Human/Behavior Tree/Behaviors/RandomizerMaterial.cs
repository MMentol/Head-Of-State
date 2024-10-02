using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RandomizerMaterial : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    public HumanStats _hStats;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public RandomizerMaterial(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        this._hStats = transform.GetComponent<HumanStats>();

    }

    public override NodeState Evaluate()
    {
        var randMat = GetData("random");


        if (randMat == null)
        {
            randMat = (int)Random.Range(1, 4);
            parent.parent.SetData("random", randMat);
            Debug.Log("rand: " + randMat);
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
