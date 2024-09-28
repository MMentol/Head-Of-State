using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMap.Resources;
using System.Linq;

using BehaviorTree;

public class lookFoodTask : Node
{
    private static int _foodSourceMask = 1 << 6;

    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    public FoodResource[] foodSources;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public lookFoodTask(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;

    }

    public override NodeState Evaluate()
    {
        this.foodSources = GameObject.FindObjectsOfType<FoodResource>();


        object t = GetData("food");
        if (t == null)
        {
            var closest = this.foodSources
            //.Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();
            var close = closest.GetComponent<FoodResource>();
            if (close == null)
                return NodeState.FAILURE;
            else
                while (close.GetRawMaterialAmount() == 0 || close.ToDestroy() || close.GetOccupied() != null)
                {
                    var list = this.foodSources.ToList();
                    list.Remove(closest);
                    foodSources = list.ToArray();
                    closest = this.foodSources
                    .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                    .FirstOrDefault();
                    close = closest.GetComponent<FoodResource>();
                    if (close == null)
                        return NodeState.FAILURE;
                }
            parent.parent.SetData("food", closest);
            state = NodeState.SUCCESS;


        }

        state = NodeState.SUCCESS;
        return state;
    }
}
