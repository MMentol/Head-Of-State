using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using GridMap.Structures.Storage;
using Scripts;
using UnityEngine;

namespace Cinaed.GOAP.Complex.Actions
{
    public class DepositMaterialAction<TMaterial, TStorage> : ActionBase<DepositMaterialAction<TMaterial, TStorage>.Data>
        where TMaterial : MaterialBase
        where TStorage : MaterialStorageBase
    {
        public override void Created()
        {
        }

        public override void End(IMonoAgent agent, Data data)
        {
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Storage == null || data.Target == null)
                return ActionRunState.Stop;

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            string type = typeof(TMaterial).Name;
            Inventory inventory = agent.GetComponent<Inventory>();
            int withdraw = data.Storage.Add(inventory.GetResourceCount(type));
            inventory.GetFromInventory(type, withdraw);

            return ActionRunState.Stop;
        }

        public override void Start(IMonoAgent agent, Data data)
        {
            if (data.Target is not TransformTarget transformTarget)
                return;

            data.Storage = transformTarget.Transform.GetComponent<MaterialStorageBase>();
            data.Timer = 3;
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public MaterialStorageBase Storage { get; set; }
            public float Timer { get; set; }
        }
    }
}