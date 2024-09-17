using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using GridMap.Resources;
using Scripts;
using UnityEngine;

namespace Cinaed.GOAP.Simple.Actions
{
    public class GatherWoodAction : ActionBase<GatherWoodAction.Data>
    {
        public override void Start(IMonoAgent agent, Data data)
        {
            if (data.Target is not TransformTarget transformTarget)
                return;

            data.Tree = transformTarget.Transform.GetComponent<TreeResource>();
            data.Timer = 5;
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Tree == null)
                return ActionRunState.Stop;

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            Inventory inventory = agent.GetComponent<Inventory>();
            Inventory targetInventory = data.Tree.GetComponent<Inventory>();
            if (inventory != null && targetInventory != null)
            {
                int harvested = data.Tree.Harvest(1);
                string resource = "wood";
                if (harvested > 0)
                    inventory.AddResource(resource, harvested);
            }
            else
                Debug.LogError("An Inventory is Missing");

            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, Data data)
        {          
            //Debug.Log("Ended Gather Wood");
        }

        public override void Created()
        {
            //Debug.Log("Created Gather Wood Action");
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public TreeResource Tree { get; set; }
            public float Timer { get; set; }
        }
    }
}
