using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;
using System.Linq;
using GridMap.Structures.Storage;

using BehaviorTree;

public class lookForMetalResourceTask : Node
{
    private static int _metalSourceMask = 1 << 6;

    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    public MetalResource[] metalSources;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookForMetalResourceTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;

    }

    public override NodeState Evaluate()
    {
        this.metalSources = GameObject.FindObjectsOfType<MetalResource>();

        object t = GetData("metal");
        if (t == null)
        {
            var closest = this.metalSources
            .Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
            var close = closest.GetComponent<MetalResource>();
            if (close == null)
                return NodeState.FAILURE;
            else
                while (close.GetRawMaterialAmount() == 0 || close.ToDestroy() || close.GetOccupied() != null)
                {
                    var list = this.metalSources.ToList();
                    list.Remove(closest);
                    metalSources = list.ToArray();
                    closest = this.metalSources
                    .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                    .FirstOrDefault();
                    close = closest.GetComponent<MetalResource>();
                    if (close == null)
                        return NodeState.FAILURE;
                }
            parent.parent.SetData("metal", closest);
            state = NodeState.SUCCESS;


        }

        state = NodeState.SUCCESS;
        return state;
    }
}
