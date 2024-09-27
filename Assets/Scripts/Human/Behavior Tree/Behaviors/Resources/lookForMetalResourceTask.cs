using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;
using System.Linq;

using BehaviorTree;

public class lookForMetalResourceTask : Node
{
    private static int _woodSourceMask = 1 << 6;

    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    public GameObject[] woodSources;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookForMetalResourceTask(Transform transform, GameObject[] Sources)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        woodSources = Sources;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("metal");
        if (t == null)
        {
            List<object> metalSource = new List<object>();
            //Look for all metalSources on the Map
            foreach (object m in metalSource)
            {
                //Look through each foodsource if it has resource
                if (m != null)
                {
                    parent.parent.SetData("metal", m);
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
