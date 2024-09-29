using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BehaviorTree;

public class lookForHomeTask : Node
{
    private Transform _transform;

    public House[] houses;
    private float _attackCounter = 0f;

    public lookForHomeTask(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        this.houses = GameObject.FindObjectsOfType<House>();


        object t = GetData("home");
        if (t == null)
        {
            var closest = this.houses
                .Where(home => home.PeopleInside.Count < home.Capacity)
                .OrderBy(x => Vector3.Distance(_transform.position, x.transform.position))
                .FirstOrDefault();
            if (closest == null) { return NodeState.FAILURE; }
            parent.parent.SetData("home", closest);
            state = NodeState.SUCCESS;


        }

        state = NodeState.FAILURE;
        return state;
    }
}
