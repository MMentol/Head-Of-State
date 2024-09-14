using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using Demos.Complex.Classes;
using Demos.Complex.Classes.Items;
using Demos.Complex.Factories.Extensions;
using Demos.Complex.Interfaces;
using Demos.Shared;

namespace Demos.Complex.Factories
{
    public class MinerGoapSetConfigFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
            var builder = new GoapSetBuilder(SetIds.Miner);
            
            // Debugger
            builder.SetAgentDebugger<AgentDebugger>();

            // Goals
            builder.AddWanderGoal();
            
            builder.AddFixHungerGoal();
            builder.AddFixThirstGoal();
            builder.AddPickupItemGoal<Pickaxe>();

            builder.AddGatherItemGoal<Iron>();
            builder.AddGatherItemGoal<Stone>();
            
            // Actions
            builder.AddWanderAction();

            builder.AddPickupItemAction<Iron>();
            builder.AddPickupItemAction<Pickaxe>();
            builder.AddPickupItemAction<IEatable>();
            builder.AddPickupItemAction<IDrinkable>();
            
            builder.AddGatherItemAction<Iron, Pickaxe>();
            builder.AddGatherItemSlowAction<Iron>();
            builder.AddGatherItemSlowAction<Stone>();

            builder.AddEatAction();
            builder.AddDrinkAction();
            
            // TargetSensors
            builder.AddWanderTargetSensor();
            builder.AddTransformTargetSensor();
            
            builder.AddClosestItemTargetSensor<Iron>();
            builder.AddClosestItemTargetSensor<Stone>();
            builder.AddClosestItemTargetSensor<Pickaxe>();
            builder.AddClosestItemTargetSensor<IEatable>();
            builder.AddClosestItemTargetSensor<IDrinkable>();
            
            builder.AddClosestSourceTargetSensor<Iron>();
            builder.AddClosestSourceTargetSensor<Stone>();

            // WorldSensors
            builder.AddIsHoldingSensor<Pickaxe>();
            builder.AddIsHoldingSensor<Iron>();
            builder.AddIsHoldingSensor<Stone>();
            builder.AddIsHoldingSensor<IEatable>();
            builder.AddIsHoldingSensor<IDrinkable>();
            
            builder.AddIsInWorldSensor<Pickaxe>();
            builder.AddIsInWorldSensor<Iron>();
            builder.AddIsInWorldSensor<IEatable>();
            builder.AddIsInWorldSensor<IDrinkable>();
            
            return builder.Build();
        }
    }
}