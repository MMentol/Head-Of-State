
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Complex.Actions
{
    public class BreedingAction : ActionBase<BreedingAction.Data>
    {
        public override void Created()
        { }

        public override void Start(IMonoAgent agent, Data data)
        {
            if (data.Target is not TransformTarget transformTarget)
                return;

            data.Human = agent.GetComponent<HumanStats>();

            House house = transformTarget.Transform.GetComponent<House>();
            if (house.PeopleInside.Count < house.Capacity)
            {
                data.House = house;
                house.UpdateCurrentHouse(data.Human);
            }

            data.Timer = 5f;
        }

        public override void End(IMonoAgent agent, Data data) { if (data.House != null) data.House.LeaveHouse(data.Human); }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Target == null)
                return ActionRunState.Stop;
            if (data.House == null)
                return ActionRunState.Stop;
            if (data.Human.breedCooldown > 0)
                return ActionRunState.Stop;

            if (data.House.EnterHouse(data.Human))
                return ActionRunState.Continue;

            if (data.Human._happiness <= data.House.HouseSettings.RequiredHappiness)
            {
                data.House.LeaveHouse(data.Human);
                return ActionRunState.Stop;
            }

            //Find partner
            data.Partner = data.House.PeopleInside.Where(h => data.House.IsHappy(h) && h != data.Human && h.breedCooldown <= 0).FirstOrDefault();

            if (data.Partner == null)
                return ActionRunState.Continue;

            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            if (data.House.MakeNewHuman(data.Human, data.Partner))
            {
                GameObject.FindObjectOfType<MaterialDataStorage>().TallyMaterials();
                float breedCooldown = 60f;
                data.Human.breedCooldown = breedCooldown;
                data.Partner.breedCooldown = breedCooldown;
                data.Human._energy = 100;
                data.Partner._energy = 100;
                data.Partner._heat = 100;

            }
            data.House.LeaveHouse(data.Human);
            data.House.LeaveHouse(data.Partner);

            return ActionRunState.Stop;
        }


        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public House House { get; set; }
            public HumanStats Human { get; set; }
            public HumanStats Partner { get; set; }
            public float Timer { get; set; }
        }
    }
}
