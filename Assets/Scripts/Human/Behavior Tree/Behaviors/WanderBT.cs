using UnityEngine;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using BehaviorTree;

public class WanderBT : Node
{
    private Animator _animator;

    private Transform _lastTarget;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;


    public WanderBT(Transform transform)
    {

        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {

        return base.Evaluate();
    }
}
