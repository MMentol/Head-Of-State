using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;
using System.Linq;

using BehaviorTree;

public class lookWaterTask : Node
{
    private static int _woodSourceMask = 1 << 6;

    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    public GameObject[] waterSources;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    public lookWaterTask(Transform transform, GameObject[] Sources)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        waterSources = Sources;
    }

    public override NodeState Evaluate()
    {

        object t = GetData("water");
        if (t == null)
        {
            var closest = this.waterSources
            //.Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
            var close = closest.GetComponent<WaterResource>();
            if (close == null)
                return NodeState.FAILURE;
            else
                while (close.GetRawMaterialAmount() == 0 || close.ToDestroy() || close.GetOccupied() != null)
                {
                    var list = this.waterSources.ToList();
                    list.Remove(closest);
                    waterSources = list.ToArray();
                    closest = this.waterSources
                    .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                    .FirstOrDefault();
                    close = closest.GetComponent<WaterResource>();
                    if (close == null)
                        return NodeState.FAILURE;
                }
            parent.parent.SetData("water", closest);
            state = NodeState.SUCCESS;


        }

        state = NodeState.SUCCESS;
        return state;
    }
}
