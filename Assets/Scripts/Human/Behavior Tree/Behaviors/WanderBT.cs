using UnityEngine;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using BehaviorTree;
using System.Collections;

public class WanderBT : Node
{
    private Animator _animator;

    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private HumanController humanController;
    private float _timerTime = 5f;
    private float _timerCounter = 0f;
    private bool _timer = false;

    Vector3 newPos;
    float rndx = 0f;
    float rndz = 0f;
    private bool firstWander = true;

    public WanderBT(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _animator = transform.GetComponent<Animator>();

        

    }

    public override NodeState Evaluate()
    {
        if (_timer)
        {
            _timerCounter += Time.deltaTime;
            if (_timerCounter >= _timerTime)
            {
                _timer = false;
            }
        }
        else
        {

            newPos = new Vector3(rndx, _transform.position.y, rndz);


            if (firstWander || Vector3.Distance(_transform.position, newPos) < 0.1f)
            {

                rndx = Mathf.Clamp(Random.Range((int)_transform.position.x - humanController.randomMoveDistance, (int)_transform.position.x + humanController.randomMoveDistance), 0, 99);
                rndz = Mathf.Clamp(Random.Range((int)_transform.position.z - humanController.randomMoveDistance, (int)_transform.position.z + humanController.randomMoveDistance), 0, 99);
                
                newPos = new Vector3(rndx, _transform.position.y, rndz);

                Debug.Log("newPos: " + newPos);
                humanController.SetTargetPosition(newPos);

                _timerCounter = 0f;
                _timer = true;
                firstWander = false;
            }
            else
            {

                
            }
        }
        

        return NodeState.RUNNING;
    }

}
