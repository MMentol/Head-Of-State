using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class sleepTask : Node
{
    private HumanController humanController;
    private Transform _transform;
    private HumanStats _hStats;


    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    public sleepTask(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        rootTree = transform.GetComponent<HumanBT>();
        _hStats = transform.GetComponent<HumanStats>();
    }

    public override NodeState Evaluate()
    {

        House houseTile = (House)GetData("home");

        if (houseTile == null || _hStats._heat>=99 || _hStats._energy>=99) return NodeState.FAILURE;

        
        if (_transform.position.Equals(houseTile.transform.position))
        {
            rootTree.currentAction = "sleep";
            //add food to human
            //remove food from tile

            //if (harvested > 0)
            //    this.inventory.addtoinventory(resource, harvested);
            houseTile.EnterHouse(_hStats);
            houseTile.UpdateCurrentHouse(_hStats);

            houseTile.NapTime(_hStats);
            houseTile.LeaveHouse(_hStats);
            state = NodeState.SUCCESS;
            //Debug.Log("stateget :" + state);
            
            return state;
        }
        state = NodeState.FAILURE;
        //Debug.Log("stateget :" + state);

        return state;
    }
}
