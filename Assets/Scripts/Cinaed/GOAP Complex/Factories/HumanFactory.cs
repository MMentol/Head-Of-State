using Cinaed.GOAP.Complex.Factories.Extensions;
using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using GridMap.Resources;

namespace Cinaed.GOAP.Complex.Factories
{
    public class HumanFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
            var builder = new GoapSetBuilder("Human");

            //Goals
            builder.AddGatherGoal<Wood>();
            builder.AddGatherGoal<Stone>();
            builder.AddGatherGoal<Metal>();
            builder.AddGatherGoal<Water>();

            //Actions
            builder.AddGatherAction<Wood, TreeResource>();
            builder.AddGatherAction<Stone, StoneResource>();
            builder.AddGatherAction<Metal, MetalResource>();
            builder.AddGatherAction<Water, WaterResource>();

            //Target Sensors
            builder.AddClosestMaterialSourceSensor<Wood, TreeResource>();
            builder.AddClosestMaterialSourceSensor<Stone, StoneResource>();
            builder.AddClosestMaterialSourceSensor<Metal, MetalResource>();
            builder.AddClosestMaterialSourceSensor<Water, WaterResource>();

            //World Sensors
            builder.AddInventorySpaceSensor();
            builder.AddMaterialAmountSensor<Wood>();
            builder.AddMaterialAmountSensor<Stone>();
            builder.AddMaterialAmountSensor<Metal>();
            builder.AddMaterialAmountSensor<Food>();
            builder.AddMaterialAmountSensor<Water>();

            return builder.Build();
        }

    }
}