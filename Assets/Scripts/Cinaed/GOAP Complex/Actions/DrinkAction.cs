using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;

namespace Cinaed.GOAP.Complex.Actions
{
    public class DrinkAction : ActionBase<DrinkAction.Data>
    {
        public override void Created() { }

        public override void End(IMonoAgent agent, Data data) { }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context) 
        {
            if (data.Target == null)
                return ActionRunState.Stop;

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            data.Stats._thirst -= 1;

            return ActionRunState.Stop;
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
}