using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class lookForStoneResourceTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookForStoneResourceTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("stone");
        if (t == null)
        {
            List<object> stoneSource = new List<object>();
            //Look for all stoneSources on the Map
            foreach (object s in stoneSource)
            {
                //Look through each stonesource if it has resource
                if (s != null)
                {
                    parent.parent.SetData("stone", s);
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
