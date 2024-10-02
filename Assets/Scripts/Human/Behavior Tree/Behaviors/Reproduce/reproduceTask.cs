using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BehaviorTree;

public class reproduceTask : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    public HumanStats _hStats;
    private Transform _transform;


    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private float timer = 2f;

    public reproduceTask(Transform transform)
    {
        _transform = transform;
        this._hStats = transform.GetComponent<HumanStats>();
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        //Find partner
        House house = (House)GetData("bhome");

        

        if (house == null)
            return NodeState.FAILURE;

        if (_hStats.breedCooldown > 0)
            return NodeState.FAILURE;
        if (house.PeopleInside.Count < house.Capacity)
        {
            house.UpdateCurrentHouse(_hStats);
        }
        if (_transform.position.Equals(house.transform.position))
        {
            house.EnterHouse(_hStats);
            Debug.Log("man1 inside");

        }
        else
        {
            return NodeState.FAILURE;
        }


        if (_hStats._happiness <= house.HouseSettings.RequiredHappiness)
        {
            house.LeaveHouse(_hStats);
            return NodeState.FAILURE;
        }

        

        HumanStats partner = house.PeopleInside.Where(h => house.IsHappy(h) && h != _hStats && h.breedCooldown <= 0).FirstOrDefault();


        if (partner == null)
        {
        Debug.Log("abort");


            return NodeState.FAILURE;

        }

        Debug.Log("Man2 inside");

        if (house.MakeNewHuman(_hStats, partner))
        {
            GameObject.FindObjectOfType<MaterialDataStorage>().TallyMaterials();
            _hStats.breedCooldown = 60;
            partner.breedCooldown = 60;
            _hStats._heat += 100;
            _hStats._energy += 100;
            partner._heat += 100;
            partner._energy += 100;
        }
        house.LeaveHouse(_hStats);
        house.LeaveHouse(partner);
        Debug.Log("Baby Made");
        ClearData("bhome");
        return NodeState.SUCCESS;
    }
}
