using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class lookForHomeTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookForHomeTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("home");
        if (t == null)
        {
            List<object> homes = new List<object>();
            //Look for all homes on the Map
            foreach (object h in homes)
            {
                //Look through each homes if it has resource
                if (h != null)
                {
                    parent.parent.SetData("home", h);
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
