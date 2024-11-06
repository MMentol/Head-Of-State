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
    private int counter = 0;
    private int randMat = -1;

    public RandomizerMaterial(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        this._hStats = transform.GetComponent<HumanStats>();
        randMat = -1;
    }

    public override NodeState Evaluate()
    {
        if (counter <1)
        {
            parent.SetData("random", randMat);
            counter++;
        }
        //parent.SetData("random", randMat);
        randMat = (int)GetData("random");
        //Debug.Log("Randomizing: " +randMat);

        if (randMat == null || randMat <=0 )
        {
            randMat = (int)Random.Range(1, 4);
            //ClearData("random");
            parent.SetData("random", randMat);
            //Debug.Log("rand: " + randMat);
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
