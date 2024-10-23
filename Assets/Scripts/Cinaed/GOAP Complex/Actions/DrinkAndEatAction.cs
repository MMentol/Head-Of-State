using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;

namespace Cinaed.GOAP.Complex.Actions
{
    public class DrinkingAction : ActionBase<DrinkingAction.Data>
    {
        public override void Created() { }

        public override void End(IMonoAgent agent, Data data) { }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context) 
        {
            if (data.Stats._thirst <= 0)
                return ActionRunState.Stop;

            if (data.Target == null)
                return ActionRunState.Stop;

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            data.Stats._thirst = Mathf.Max(data.Stats._thirst - 25, 0);
            data.Timer = 1f;

            if (data.Stats._thirst <= 0)
                return ActionRunState.Stop;

            return ActionRunState.Continue;
        }

        public override void Start(IMonoAgent agent, Data data) 
        {
            if (data.Target is not TransformTarget transformTarget)
                return;
            data.Stats = agent.GetComponent<HumanStats>();
            data.Timer = 1f;
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public float Timer { get; set; }
            public HumanStats Stats { get; set; }
        }
    }

    public class EatingAction : ActionBase<EatingAction.Data>
    {
        public override void Created() { }

        public override void End(IMonoAgent agent, Data data) { }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Stats._hunger <= 0)
                return ActionRunState.Stop;

            if (data.Target == null)
                return ActionRunState.Stop;

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            data.Stats._hunger = Mathf.Max(data.Stats._hunger - 25, 0);
            data.Timer = 1f;

            if (data.Stats._hunger <= 0)
                return ActionRunState.Stop;

            return ActionRunState.Continue;

        }

        public override void Start(IMonoAgent agent, Data data)
        {
            if (data.Target is not TransformTarget transformTarget)
                return;
            data.Stats = agent.GetComponent<HumanStats>();
            data.Timer = 1f;
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public float Timer { get; set; }
            public HumanStats Stats { get; set; }
        }
    }

    public class RestingAction : ActionBase<RestingAction.Data>
    {
        public override void Created() { }

        public override void End(IMonoAgent agent, Data data) { if (data.House != null) data.House.LeaveHouse(data.Stats); }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Target == null)
                return ActionRunState.Stop;
            if (data.House == null)
                return ActionRunState.Stop;
            if (data.House != data.Stats.currentHouse)
            {
                //Debug.Log($"Data: {data.House} Cur:{data.Stats.currentHouse}");
                return ActionRunState.Stop;
            }

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            data.House.EnterHouse(data.Stats);
            data.House.NapTime(data.Stats);
            data.House.LeaveHouse(data.Stats);

            return ActionRunState.Stop;
        }

        public override void Start(IMonoAgent agent, Data data)
        {
            if (data.Target is not TransformTarget transformTarget)
                return;

            data.Stats = agent.GetComponent<HumanStats>();

            House house = transformTarget.Transform.GetComponent<House>();
            if (house.PeopleInside.Count < house.Capacity)
            {
                data.House = house;
                house.UpdateCurrentHouse(data.Stats);
            }

            data.Timer = 5f;
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public House House { get; set; }
            public float Timer { get; set; }
            public HumanStats Stats { get; set; }
        }
    }
}