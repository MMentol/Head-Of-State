using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;
using System.Linq;

using BehaviorTree;

public class lookWaterTask : Node
{
    private static int _waterSourceMask = 1 << 6;

    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    public WaterResource[] waterSources;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookWaterTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;

    }

    public override NodeState Evaluate()
    {
        this.waterSources = MaterialDataStorage.Instance.GetSourceOfType<WaterResource>();


        object t = GetData("water");
        if (t == null)
        {
            var closest = this.waterSources
            .Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
            if (closest == null)
                return NodeState.FAILURE;
            //else
            //    while (closest.GetRawMaterialAmount() == 0 || closest.ToDestroy() || closest.GetOccupied() != null)
            //    {
            //        var list = this.waterSources.ToList();
            //        list.Remove(closest);
            //        waterSources = list.ToArray();
            //        closest = this.waterSources
            //        .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            //        .FirstOrDefault();
            //        if (closest == null)
            //            return NodeState.FAILURE;
            //    }
            parent.parent.SetData("water", closest);
            state = NodeState.SUCCESS;


        }

        state = NodeState.SUCCESS;
        return state;
    }
}
