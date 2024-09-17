using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
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
            Debug.Log("Start GatherWood");
            if (data.Target is not TransformTarget transformTarget)
                return;

            data.Tree = transformTarget.Transform.GetComponent<TreeResource>();
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Tree == null)
                return ActionRunState.Stop;

            data.Progress += context.DeltaTime * 10f;

            if (data.Progress < 0)
                return ActionRunState.Continue;

            Inventory inventory = agent.GetComponent<Inventory>();
            Inventory treeInventory = data.Tree.GetComponent<Inventory>();
            if (inventory != null)
            {
                string resource = "wood";
                if (treeInventory != null)
                    inventory.AddResource(resource, treeInventory.GetResource(resource, 1));
                else
                    Debug.Log("Tree Inventory Missing");
            }
                

            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, Data data)
        {
        }

        public override void Created()
        {
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public TreeResource Tree { get; set; }
            public float Progress { get; set; } = 0f;
        }
    }
}
