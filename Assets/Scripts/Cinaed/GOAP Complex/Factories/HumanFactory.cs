using Cinaed.GOAP.Complex.Factories.Extensions;
using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using Demos.Complex.Factories.Extensions;
using GridMap.Resources;
using GridMap.Structures.Storage;
using Items;

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
            builder.AddGatherGoal<Food>(MaterialPercentage.NPCFoodThreshold);            

            //Actions
            //Gather with Tool
            builder.AddGatherAction<Wood, TreeResource, Pickaxe>();
            builder.AddGatherAction<Stone, StoneResource, Pickaxe>();
            builder.AddGatherAction<Metal, MetalResource, Pickaxe>();
            builder.AddGatherAction<Water, WaterResource, Bucket>();
            builder.AddGatherAction<Food, FoodResource, Pickaxe>();
            //Slow Gather
            builder.AddHandGatherAction<Wood, TreeResource>();
            builder.AddHandGatherAction<Stone, StoneResource>();
            builder.AddHandGatherAction<Metal, MetalResource>();
            builder.AddHandGatherAction<Water, WaterResource>();

            //Target Sensors
            builder.AddClosestMaterialSourceSensor<Wood, TreeResource>();
            builder.AddClosestMaterialSourceSensor<Stone, StoneResource>();
            builder.AddClosestMaterialSourceSensor<Metal, MetalResource>();
            builder.AddClosestMaterialSourceSensor<Water, WaterResource>();
            builder.AddClosestMaterialSourceSensor<Food, FoodResource>();

            //World Sensors
            builder.AddInventorySpaceSensor();
            builder.AddAgentMaterialAmountSensor<Wood>();
            builder.AddAgentMaterialAmountSensor<Stone>();
            builder.AddAgentMaterialAmountSensor<Metal>();
            builder.AddAgentMaterialAmountSensor<Food>();
            builder.AddAgentMaterialAmountSensor<Water>();

            //Deposit
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

            //World Sensors
            /*builder.AddStorageSpaceSensor<Wood>();
            builder.AddStorageSpaceSensor<Stone>();
            builder.AddStorageSpaceSensor<Metal>();
            builder.AddStorageSpaceSensor<Food>();
            builder.AddStorageSpaceSensor<Water>();*/

            builder.AddPlayerMaterialPercentageSensor<Wood>();
            builder.AddPlayerMaterialPercentageSensor<Stone>();
            builder.AddPlayerMaterialPercentageSensor<Metal>();
            builder.AddPlayerMaterialPercentageSensor<Food>();
            builder.AddPlayerMaterialPercentageSensor<Water>();


            //Craft
            builder.AddCraftItemGoal<Pickaxe>();
            builder.AddCraftItemGoal<Bucket>();
            //Actions
            builder.AddCraftingAction<Pickaxe>();
            builder.AddCraftingAction<Bucket>();
            //Target Sensors
            builder.AddSelfTargetSensor();
            //World Sensors
            builder.AddItemAmountSensor<Pickaxe>();
            builder.AddItemAmountSensor<Bucket>();
            //builder.AddIsItemCraftableSensor<Pickaxe>();
            //BUILD GOAP SET
            return builder.Build();
        }

    }
}