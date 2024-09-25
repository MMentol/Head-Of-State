using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using Items;
using Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Cinaed.GOAP.Complex.Actions
{
    public class CraftItemAction<TCraftable> : ActionBase<CraftItemAction<TCraftable>.Data>
        where TCraftable : ItemBase
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

            Inventory inventory = agent.GetComponent<Inventory>();
            if (data.ItemType.CraftItem(inventory))
            {
                //List<ItemBase> items = inventory.items;
                //items.Add(new TCraftable());
                Debug.Log($"Crafted Item.");
            }

            return ActionRunState.Stop;
        }

        public override void Start(IMonoAgent agent, Data data)
        {
            if (data.Target is not TransformTarget transformTarget)
                return;
            Debug.Log($"Start CreateItem Action");
            data.ItemType = GameObject.FindObjectOfType<TCraftable>();
            data.Timer = 3;
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public ItemBase ItemType { get; set; }
            public float Timer { get; set; }
        }
    }
}
