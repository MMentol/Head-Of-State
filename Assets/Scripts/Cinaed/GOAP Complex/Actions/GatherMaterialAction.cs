using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using GridMap.Resources;
using Scripts;
using UnityEngine;

namespace Cinaed.GOAP.Complex.Actions
{
    public class GatherMaterialAction<TMaterial, TSource> : ActionBase<GatherMaterialAction<TMaterial, TSource>.Data>
        where TMaterial : MaterialBase
        where TSource : ResourceSourceBase
    {
        public override void Start(IMonoAgent agent, Data data)
        {
            if (data.Target is not TransformTarget transformTarget)
                return;

            data.Source = transformTarget.Transform.GetComponent<ResourceSourceBase>();
            data.Source.SetOccupied(agent.gameObject);
            data.Timer = 1;
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Target == null)
                return ActionRunState.Stop;
            if (data.Source == null)
                return ActionRunState.Stop;
            /*if (data.Source.GetOccupied() != agent.gameObject)
                return ActionRunState.Stop;*/
            if (data.Source.ToDestroy())
                return ActionRunState.Stop;

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            if (data.Target == null || data.Source == null || data.Source.ToDestroy())
                return ActionRunState.Stop;

            Inventory inventory = agent.GetComponent<Inventory>();
            if (inventory != null)
            {
                int harvested = data.Source.Harvest(1);
                string resource = typeof(TMaterial).Name.ToLower();
                if (harvested > 0)
                    inventory.AddToInventory(resource, harvested);
            }
            else
                Debug.LogError($"{agent.name} Inventory is Missing");
            data.Source.RemoveOccupied();

            //Debug.Log("Success Gather Action");
            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, Data data)
        {           
        }

        public override void Created()
        {
            //Debug.Log("Created Gather Wood Action");
        }

        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public ResourceSourceBase Source { get; set; }
            public float Timer { get; set; }
        }
    }
}
