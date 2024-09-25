using System.Linq;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using Demos.Complex.Behaviours;
using Demos.Complex.Goap;
using Demos.Complex.Interfaces;
using Demos.Shared.Behaviours;

namespace Demos.Complex.Actions
{
    public class DrinkAction : ActionBase<DrinkAction.Data>, IInjectable
    {
        private InstanceHandler instanceHandler;

        public void Inject(GoapInjector injector)
        {
            this.instanceHandler = injector.instanceHandler;
        }

        public override void Created()
        {
        }

        public override void Start(IMonoAgent agent, Data data)
        {
            data.Drinkable = data.Inventory.Get<IDrinkable>().FirstOrDefault();
            data.Inventory.Hold(data.Drinkable);
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            if (data.Drinkable== null)
                return ActionRunState.Stop;
            
            var drinkThirst = context.DeltaTime * 20f;
            data.Drinkable.ThirstValue -= drinkThirst;
            data.Thirst.thirst -= drinkThirst;

            if (data.Thirst.thirst <= 20f)
                return ActionRunState.Stop;

            if (data.Drinkable.ThirstValue > 0)
                return ActionRunState.Continue;

            if (data.Drinkable == null)
                return ActionRunState.Stop;
            
            data.Inventory.Remove(data.Drinkable);
            this.instanceHandler.QueueForDestroy(data.Drinkable);
            
            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, Data data)
        {
            if (data.Drinkable == null)
                return;
            
            if (data.Drinkable.ThirstValue > 0)
                data.Inventory.Add(data.Drinkable);
        }
        
        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public IDrinkable Drinkable { get; set; }
            
            [GetComponent]
            public ComplexInventoryBehaviour Inventory { get; set; }
            
            [GetComponent]
            public ThirstBehaviour Thirst { get; set; }
        }
    }
}