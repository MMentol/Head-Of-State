using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BehaviorTree;
using GridMap.Structures.Storage;

public class checkForWoodStorageTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private WoodStorage[] woodStorage;

    public checkForWoodStorageTask(Transform transform)
    {
        this._animator = transform.GetComponent<Animator>();
        this._transform = transform;
    }

    
    public override NodeState Evaluate()
    {
        this.woodStorage = MaterialDataStorage.Instance.GetStoragesOfType<WoodStorage>();

        // Debug.Log("checking: " +this.woodStorage + " " + woodStorage.Length);

        if (woodStorage == null || woodStorage.Length <= 1 )
        {
            state = NodeState.FAILURE;
            //Debug.Log("rete:  " + (int)GetData("random"));
            ClearData("random");

            parent.parent.parent.parent.SetData("random", -1);
            //Debug.Log("rete2:  " + (int)GetData("random"));



            return state;
        }
        var closest = this.woodStorage
            .Where(x => x.Capacity > x.Count)
            .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
            .FirstOrDefault();

        if (closest == null)
        {
            ClearData("random");

            parent.parent.parent.parent.SetData("random", -1);



            state = NodeState.FAILURE; 
           //Debug.Log("checking: " + state);

            return state;
        }
        else
            while (closest.Capacity <= closest.Count)
            {
                var list = this.woodStorage.ToList();
                list.Remove(closest);
                woodStorage = list.ToArray();
                closest = this.woodStorage
                .OrderBy(x => Vector3.Distance(x.transform.position, _transform.position))
                .FirstOrDefault();
                if (closest == null)
                    return NodeState.FAILURE;
            }

        parent.parent.SetData("woodStorage", closest);

        state = NodeState.SUCCESS;

        return state;
    }
}
