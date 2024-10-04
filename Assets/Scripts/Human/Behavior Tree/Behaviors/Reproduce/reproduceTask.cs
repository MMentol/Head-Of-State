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
    }

    public override NodeState Evaluate()
    {
        //Find partner
        House house = (House)GetData("bhome");



        if (house == null)
            return NodeState.FAILURE;

        //if (_hStats.breedCooldown > 0)
        //    return NodeState.FAILURE;

        if (_transform.position.Equals(house.transform.position))
        {
            if (house.PeopleInside.Count < house.Capacity && _hStats.insideHouse == 0f)
            {
                house.UpdateCurrentHouse(_hStats);
            }

            if (_hStats.insideHouse == 0f)
            {
                house.EnterHouse(_hStats);
                _hStats.insideHouse = 1;
                Debug.Log("man1 inside");
            }


            if (_hStats._happiness <= house.HouseSettings.RequiredHappiness)
            {
                house.LeaveHouse(_hStats);
                _hStats.insideHouse = 0;
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
                MaterialDataStorage.Instance.TallyMaterials();
                _hStats.breedCooldown = 60;
                partner.breedCooldown = 60;
                _hStats._heat += 100;
                _hStats._energy += 100;
                partner._heat += 100;
                partner._energy += 100;
            }
            house.LeaveHouse(_hStats);
            house.LeaveHouse(partner);
            _hStats.insideHouse = 0;
            Debug.Log("Baby Made");
            ClearData("bhome");
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;

    }
}
