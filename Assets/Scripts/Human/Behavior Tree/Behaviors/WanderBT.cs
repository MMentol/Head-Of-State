using UnityEngine;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using BehaviorTree;

public class WanderBT : Node
{
    private Animator _animator;

    private Transform _transform;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private HumanController humanController;


    public WanderBT(Transform transform)
    {
        _transform = transform;
        humanController = transform.GetComponent<HumanController>();
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        int rndx = Mathf.Clamp(Random.Range((int)_transform.position.x - humanController.randomMoveDistance, (int)_transform.position.x - humanController.randomMoveDistance), 0, 99);
        int rndz = Mathf.Clamp(Random.Range((int)_transform.position.z - humanController.randomMoveDistance, (int)_transform.position.z - humanController.randomMoveDistance),0,99);


        humanController.SetTargetPosition(new Vector3(rndx,_transform.position.y,rndz));

        return NodeState.RUNNING;
    }
}
