using Cinaed.GOAP.Complex.Factories.Extensions;
using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using Demos.Complex.Factories.Extensions;
using GridMap.Resources;
using GridMap.Structures.Storage;

namespace Cinaed.GOAP.Complex.Factories
{
    public class HumanFactory : GoapSetFactoryBase
    {
        MaterialPercentage MaterialPercentage;

        public override IGoapSetConfig Create()
        {
            MaterialPercentage = gameObject.GetComponent<MaterialPercentage>();
            var builder = new GoapSetBuilder("Human");
            
            //Wandering
            builder.AddWanderGoal();
            builder.AddWanderAction();
            builder.AddWanderTargetSensor();

            //Gathering
            //Goals           
            builder.AddGatherGoal<Wood>(MaterialPercentage.NPCWoodThreshold);
            builder.AddGatherGoal<Stone>(MaterialPercentage.NPCStoneThreshold);
            builder.AddGatherGoal<Metal>(MaterialPercentage.NPCMetalThreshold);
            builder.AddGatherGoal<Water>(MaterialPercentage.NPCWaterThreshold);            

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
            builder.AddAgentMaterialAmountSensor<Wood>();
            builder.AddAgentMaterialAmountSensor<Stone>();
            builder.AddAgentMaterialAmountSensor<Metal>();
            builder.AddAgentMaterialAmountSensor<Food>();
            builder.AddAgentMaterialAmountSensor<Water>();

            //Deposit
            //Goals
            /*builder.AddDepositGoal<Wood>();
            builder.AddDepositGoal<Stone>();
            builder.AddDepositGoal<Metal>();
            builder.AddDepositGoal<Water>();
            builder.AddDepositGoal<Metal>();*/
            //Actions
            builder.AddDepositAction<Wood, WoodStorage>(MaterialPercentage.NPCWoodThreshold);
            builder.AddDepositAction<Stone, StoneStorage>(MaterialPercentage.NPCStoneThreshold);
            builder.AddDepositAction<Metal, MetalStorage>(MaterialPercentage.NPCMetalThreshold);
            builder.AddDepositAction<Food, FoodStorage>(MaterialPercentage.NPCFoodThreshold);
            builder.AddDepositAction<Water, WaterStorage>(MaterialPercentage.NPCWaterThreshold);
            //Target Sensors
            builder.AddClosestStorageWithSpaceSensor<Wood, WoodStorage>();
            builder.AddClosestStorageWithSpaceSensor<Stone, StoneStorage>();
            builder.AddClosestStorageWithSpaceSensor<Metal, MetalStorage>();
            builder.AddClosestStorageWithSpaceSensor<Food, FoodStorage>();
            builder.AddClosestStorageWithSpaceSensor<Water, WaterStorage>();

            builder.AddStorageSpaceSensor<Wood>();
            builder.AddStorageSpaceSensor<Stone>();
            builder.AddStorageSpaceSensor<Metal>();
            builder.AddStorageSpaceSensor<Food>();
            builder.AddStorageSpaceSensor<Water>();
            //World Sensors
            builder.AddStorageSpaceSensor<Wood>();
            builder.AddStorageSpaceSensor<Stone>();
            builder.AddStorageSpaceSensor<Metal>();
            builder.AddStorageSpaceSensor<Food>();
            builder.AddStorageSpaceSensor<Water>();

            builder.AddPlayerMaterialAmountSensor<Wood>();
            builder.AddPlayerMaterialAmountSensor<Stone>();
            builder.AddPlayerMaterialAmountSensor<Metal>();
            builder.AddPlayerMaterialAmountSensor<Food>();
            builder.AddPlayerMaterialAmountSensor<Water>();

            return builder.Build();
        }

    }
}