using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;
using System.Linq;

using BehaviorTree;

public class lookForWoodResourceTask : Node
{
    private static int _woodSourceMask = 1 << 6;

    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    public GameObject[] woodSources;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookForWoodResourceTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        
    }

    public override NodeState Evaluate()
    {
        woodSources = GameObject.FindGameObjectsWithTag("WoodSource");

        object t = GetData("wood");
        if (t == null)
        {
            var closest = this.woodSources
            //.Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
            var close = closest.GetComponent<TreeResource>();
            if (close == null)
                return NodeState.FAILURE;
            else
                while (close.GetRawMaterialAmount() == 0 || close.ToDestroy() || close.GetOccupied() != null)
                {
                    var list = this.woodSources.ToList();
                    list.Remove(closest);
                    woodSources = list.ToArray();
                    closest = this.woodSources
                    .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                    .FirstOrDefault();
                    close = closest.GetComponent<TreeResource>();
                    if (close == null)
                        return NodeState.FAILURE;
                }
            parent.parent.SetData("wood", closest);
            state = NodeState.SUCCESS;
            

        }

        state = NodeState.SUCCESS;
        return state;
    }
}
