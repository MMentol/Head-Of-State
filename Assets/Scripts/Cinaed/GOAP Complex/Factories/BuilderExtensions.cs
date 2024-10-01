using Cinaed.GOAP.Complex.Actions;
using Cinaed.GOAP.Complex.Goals;
using Cinaed.GOAP.Complex.Interfaces;
using Cinaed.GOAP.Complex.Target;
using Cinaed.GOAP.Complex.TargetKeys;
using Cinaed.GOAP.Complex.Targets;
using Cinaed.GOAP.Complex.TargetSensors;
using Cinaed.GOAP.Complex.WorldKeys;
using Cinaed.GOAP.Complex.WorldKeySensors;
using Cinaed.GOAP.WorldSensors;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using GridMap.Resources;
using GridMap.Structures.Storage;
using Items;
using System.Collections.Generic;
using UnityEngine;

namespace Cinaed.GOAP.Complex.Factories.Extensions
{
    public static class GatheringExtensions
    {
        //GOAL EXTENSIONS
        public static void AddGatherGoal<TMaterial>(this GoapSetBuilder builder, int condition)
            where TMaterial : MaterialBase
            
        {
            builder.AddGoal<GatherMaterialGoal<TMaterial>>()
                .AddCondition<PlayerMaterialPercentage<TMaterial>>(Comparison.GreaterThanOrEqual, condition);
        }

        //ACTION EXTENSIONS
        public static void AddGatherAction<TMaterial, TSource, TItem>(this GoapSetBuilder builder)
            where TMaterial: MaterialBase
            where TSource : ResourceSourceBase
            where TItem : ItemBase
        {
            builder.AddAction<GatherMaterialAction<TMaterial, TSource>>()
                .AddCondition<InventorySpaceAmount>(Comparison.GreaterThan, 0)
                .AddCondition<ItemAmount<TItem>>(Comparison.GreaterThan, 0)
                .SetTarget<ClosestMaterialSource<TMaterial>>()
                .AddEffect<AgentMaterialAmount<TMaterial>>(EffectType.Increase);
                //.AddEffect<InventorySpaceAmount>(EffectType.Decrease);
        }
        public static void AddHandGatherAction<TMaterial, TSource>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
            where TSource : ResourceSourceBase
        {
            builder.AddAction<GatherMaterialAction<TMaterial, TSource>>()
                .AddCondition<InventorySpaceAmount>(Comparison.GreaterThan, 0)
                .SetTarget<ClosestMaterialSource<TMaterial>>()
                .AddEffect<AgentMaterialAmount<TMaterial>>(EffectType.Increase)
                .SetBaseCost(5);
            //.AddEffect<InventorySpaceAmount>(EffectType.Decrease);
        }
        //TARGET SENSOR EXTENSIONS
        public static void AddClosestMaterialSourceSensor<TMaterial, TSource>(this GoapSetBuilder builder) 
            where TMaterial : MaterialBase
            where TSource : ResourceSourceBase
        {
            builder.AddTargetSensor<ClosestMaterialSourceSensor<TSource>>()
                .SetTarget<ClosestMaterialSource<TMaterial>>();
        }
        //WORLD SENSOR EXTENSIONS
        public static void AddAgentMaterialAmountSensor<TMaterial>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
        {
            builder.AddWorldSensor<AgentMaterialAmountSensor<TMaterial>>()
                .SetKey<AgentMaterialAmount<TMaterial>>();
        }
        public static void AddInventorySpaceSensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<InventorySpaceSensor>()
                .SetKey<InventorySpaceAmount>();
        }
    }

    public static class DepositExtensions
    {
        //GOAL EXTENSIONS
        public static void AddDepositGoal<TMaterial>(this GoapSetBuilder builder, int condition)
            where TMaterial : MaterialBase
        {
            builder.AddGoal<DepositMaterialGoal<TMaterial>>()
                .AddCondition<AgentMaterialAmount<TMaterial>>(Comparison.SmallerThanOrEqual, 0)
                .AddCondition<PlayerMaterialPercentage<TMaterial>>(Comparison.GreaterThanOrEqual, condition);
        }
        //ACTION EXTENSIONS
        public static void AddDepositAction<TMaterial, TStorage>(this GoapSetBuilder builder, int condition)
            where TMaterial : MaterialBase
            where TStorage : MaterialStorageBase
        {
            builder.AddAction<DepositMaterialAction<TMaterial, TStorage>>()
                .AddCondition<AgentMaterialAmount<TMaterial>>(Comparison.GreaterThan, 0)
                .SetTarget<ClosestStorageWithSpace<TMaterial>>()
                .AddEffect<AgentMaterialAmount<TMaterial>>(EffectType.Decrease)
                .AddEffect<PlayerMaterialPercentage<TMaterial>>(EffectType.Increase);
        }
        //TARGET SENSOR EXTENSIONS
        public static void AddClosestStorageWithSpaceSensor<TMaterial, TStorage>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
            where TStorage : MaterialStorageBase
        {
            builder.AddTargetSensor<ClosestStorageWithSpaceSensor<TStorage>>()
                .SetTarget<ClosestStorageWithSpace<TMaterial>>();
        }
        //WORLD SENSOR EXTENSIONS
        public static void AddStorageSpaceSensor<TMaterial>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
        {
            builder.AddWorldSensor<StorageSpaceSensor<TMaterial>>()
                .SetKey<StorageSpaceAmount<TMaterial>>();
        }
        public static void AddPlayerMaterialPercentageSensor<TMaterial>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
        {
            builder.AddWorldSensor<PlayerMaterialPercentageSensor<TMaterial>>()
                .SetKey<PlayerMaterialPercentage<TMaterial>>();
        }
        public static void AddPlayerMaterialAmountSensor<TMaterial>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
        {
            builder.AddWorldSensor<PlayerMaterialAmountSensor<TMaterial>>()
                .SetKey<PlayerMaterialAmount<TMaterial>>();
        }
    }
    public static class CraftingExtensions
    {
        //GOAL
        public static void AddCraftItemGoal<TCraftable>(this GoapSetBuilder builder)
            where TCraftable : ItemBase
        {
            builder.AddGoal<CraftItemGoal<TCraftable>>()
                .AddCondition<ItemAmount<TCraftable>>(Comparison.GreaterThan,0);
        }
        //ACTION
        public static void AddCraftingAction<TCraftable>(this GoapSetBuilder builder)
            where TCraftable : ItemBase
        {
            var action = builder.AddAction<CraftItemAction<TCraftable>>()
                .SetTarget<SelfTarget>()
                //.AddCondition<IsItemCraftable<TCraftable>>(Comparison.GreaterThan, 0)
                .AddEffect<ItemAmount<TCraftable>>(EffectType.Increase);

            ItemBase item = GameObject.FindObjectOfType<TCraftable>();
            //if (item != null) { Debug.Log(item); }
            Dictionary<string, int> recipe = item.craftingRecipe;
            if (recipe.ContainsKey("wood"))
                action.AddCondition<AgentMaterialAmount<Wood>>(Comparison.GreaterThanOrEqual, recipe["wood"]);
            if (recipe.ContainsKey("stone"))
                action.AddCondition<AgentMaterialAmount<Stone>>(Comparison.GreaterThanOrEqual, recipe["stone"]);
            if (recipe.ContainsKey("metal")) 
                action.AddCondition<AgentMaterialAmount<Metal>>(Comparison.GreaterThanOrEqual, recipe["metal"]);
        }
        //TARGET SENSOR
        public static void AddSelfTargetSensor(this GoapSetBuilder builder)
        {
            builder.AddTargetSensor<SelfTargetSensor>()
                .SetTarget<SelfTarget>();
        }
        //WORLD SENSOR
        public static void AddItemAmountSensor<TCraftable>(this GoapSetBuilder builder)
            where TCraftable : ItemBase
        {
            builder.AddWorldSensor<ItemAmountSensor<TCraftable>>()
                .SetKey<ItemAmount<TCraftable>>();
        }
        public static void AddIsItemCraftableSensor<TCraftable>(this GoapSetBuilder builder)
            where TCraftable : ItemBase
        {
            builder.AddWorldSensor<IsItemCraftableSensor<TCraftable>>()
                .SetKey<IsItemCraftable<TCraftable>>();
        }
    }
    public static class HumanStatExtensions
    {
        //GOAL
        public static void AddDrinkGoal(this GoapSetBuilder builder)
        {
            builder.AddGoal<DrinkGoal>()
                .AddCondition<Thirst>(Comparison.SmallerThanOrEqual, 0);
        }
        public static void AddEatGoal(this GoapSetBuilder builder)
        {
            builder.AddGoal<EatGoal>()
                .AddCondition<Hunger>(Comparison.SmallerThanOrEqual, 0);
        }
        public static void AddRestGoal(this GoapSetBuilder builder)
        {
            builder.AddGoal<RestGoal>()
                .AddCondition<Energy>(Comparison.GreaterThan, 0);
        }
        //ACTION
        public static void AddDrinkWaterAction(this GoapSetBuilder builder)
        {
            builder.AddAction<DrinkingAction>()
                .SetTarget<DrinkableWaterSource>()
                .AddEffect<Thirst>(EffectType.Decrease);
        }
        public static void AddEatFoodAction(this GoapSetBuilder builder)
        {
            builder.AddAction<EatingAction>()
                .SetTarget<EdibleFoodSource>()
                .AddEffect<Hunger>(EffectType.Decrease);
        }
        public static void AddRestAction(this GoapSetBuilder builder)
        {
            builder.AddAction<RestingAction>()
                .SetTarget<ClosestHouseWithSpace>()
                .AddEffect<Energy>(EffectType.Increase);
        }
        //TARGET
        public static void AddDrinkableWaterSensor(this GoapSetBuilder builder)
        {
            builder.AddTargetSensor<DrinkableWaterSourceSensor>()
                .SetTarget<DrinkableWaterSource>();
        }
        public static void AddFoodSourceSensor(this GoapSetBuilder builder)
        {
            builder.AddTargetSensor<EdibleFoodSourceSensor>()
                .SetTarget<EdibleFoodSource>();
        }
        //WORLD
        public static void AddThirstSensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<ThirstSensor>()
                .SetKey<Thirst>();
        }
        public static void AddHungerSensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<HungerSensor>()
                .SetKey<Hunger>();
        }
        public static void AddEnergySensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<EnergySensor>()
                .SetKey<Energy>();
        }
    }
    public static class ReproductionExtensions
    {
        //GOAL
        public static void AddBreedGoal(this GoapSetBuilder builder)
        {
            builder.AddGoal<BreedingGoal>()
                .AddCondition<PlayerMaterialPercentage<Population>>(Comparison.GreaterThanOrEqual, 100);
        }
        //ACTION
        public static void AddBreedAction(this GoapSetBuilder builder, float condition)
        {
            builder.AddAction<BreedingAction>()
                .AddCondition<Happiness>(Comparison.GreaterThan, Mathf.RoundToInt(condition))
                .AddCondition<BreedCooldown>(Comparison.SmallerThanOrEqual, 0)
                .SetTarget<ClosestHouseWithSpace>()
                .AddEffect<PlayerMaterialPercentage<Population>>(EffectType.Increase);
        }
        //TARGET
        public static void AddHouseSensor(this GoapSetBuilder builder)
        {
            builder.AddTargetSensor<ClosestHouseSensor>()
                .SetTarget<ClosestHouseWithSpace>();
        }
        //WORLD
        public static void AddHappinessSensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<HappinessSensor>()
                .SetKey<Happiness>();
        }
        public static void AddBreedCooldownSensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<BreedCooldownSensor>()
                .SetKey<BreedCooldown>();
        }
    }
}