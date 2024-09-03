using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class lookForWoodResourceTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookForWoodResourceTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("wood");
        if (t == null)
        {
            List<object> woodSource = new List<object>();
            //Look for all woodSources on the Map
            foreach (object wo in woodSource)
            {
                //Look through each woodsource if it has resource
                if (wo != null)
                {
                    parent.parent.SetData("wood", wo);
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
            state = NodeState.FAILURE;
            return state;

        }

        state = NodeState.SUCCESS;
        return state;
    }
}
