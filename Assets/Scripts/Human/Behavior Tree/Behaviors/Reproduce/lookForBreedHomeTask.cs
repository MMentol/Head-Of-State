using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BehaviorTree;

public class lookForBreedHomeTask : Node
{
    private Transform _transform;

    public House[] houses;
    private float _attackCounter = 0f;

    public lookForBreedHomeTask(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        this.houses = MaterialDataStorage.Instance.Houses;

        Debug.Log("house " + houses.Length);
        object t = GetData("bhome");
        if (t == null)
        {

            var closest = houses
                .Where(home => home.PeopleInside.Count < home.Capacity)
                .OrderBy(x => Vector3.Distance(_transform.position, x.transform.position))
                .FirstOrDefault();
            if (closest == null) { return NodeState.FAILURE; }
            parent.parent.SetData("bhome", closest);
            Debug.Log("closes: " + closest);
            state = NodeState.SUCCESS;


        }

        state = NodeState.SUCCESS;
        return state;
    }
}
