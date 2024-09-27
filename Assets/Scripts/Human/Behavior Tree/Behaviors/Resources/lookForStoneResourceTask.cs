using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;
using System.Linq;

using BehaviorTree;

public class lookForStoneResourceTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    public GameObject[] stoneSources;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookForStoneResourceTask(Transform transform, GameObject[] Sources)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        stoneSources = Sources;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("stone");
        if (t == null)
        {
            var closest = this.stoneSources
            //.Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
            var close = closest.GetComponent<StoneResource>();
            if (close == null)
                return NodeState.FAILURE;
            else
                while (close.GetRawMaterialAmount() == 0 || close.ToDestroy() || close.GetOccupied() != null)
                {
                    var list = this.stoneSources.ToList();
                    list.Remove(closest);
                    stoneSources = list.ToArray();
                    closest = this.stoneSources
                    .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                    .FirstOrDefault();
                    close = closest.GetComponent<StoneResource>();
                    if (close == null)
                        return NodeState.FAILURE;
                }
            parent.parent.SetData("stone", closest);
            state = NodeState.SUCCESS;


        }

        state = NodeState.SUCCESS;
        return state;
    }
}
