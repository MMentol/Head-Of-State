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
    private int randMat = -1;

    public RandomizerMaterial(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        this._hStats = transform.GetComponent<HumanStats>();
        randMat = -1; ;
    }

    public override NodeState Evaluate()
    {
        parent.parent.SetData("random", randMat);
        randMat = (int)GetData("random");


        if (randMat == null || randMat == -1)
        {
            randMat = (int)Random.Range(1, 4);
            parent.parent.SetData("random", randMat);
            Debug.Log("rand: " + randMat);
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
