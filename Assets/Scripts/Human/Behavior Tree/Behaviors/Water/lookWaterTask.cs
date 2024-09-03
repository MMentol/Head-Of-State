using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class lookWaterTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookWaterTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("water");
        if (t == null)
        {
            List<object> waterSource = new List<object>();
            //Look for all waterSources on the Map
            foreach (object wa in waterSource)
            {
                //Look through each watersource if it has resource
                if (wa != null)
                {
                    parent.parent.SetData("water", wa);
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
