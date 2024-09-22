using Cinaed.GOAP.Complex.Actions;
using Cinaed.GOAP.Complex.Goals;
using Cinaed.GOAP.Complex.Interfaces;
using Cinaed.GOAP.Complex.TargetSensors;
using Cinaed.GOAP.Complex.WorldKeys;
using Cinaed.GOAP.Complex.WorldKeySensors;
using Cinaed.GOAP.WorldSensors;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using Demos.Complex.Targets;
using GridMap.Resources;
using GridMap.Structures.Storage;
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
                .AddCondition<PlayerMaterialAmount<TMaterial>>(Comparison.GreaterThanOrEqual, condition);
        }

        //ACTION EXTENSIONS
        public static void AddGatherAction<TMaterial, TSource>(this GoapSetBuilder builder)
            where TMaterial: MaterialBase
            where TSource : ResourceSourceBase
        {
            builder.AddAction<GatherMaterialAction<TMaterial, TSource>>()
                .AddCondition<InventorySpaceAmount>(Comparison.GreaterThan, 0)
                .SetTarget<ClosestMaterialSource<TMaterial>>()
                .AddEffect<AgentMaterialAmount<TMaterial>>(EffectType.Increase)
                .AddEffect<InventorySpaceAmount>(EffectType.Decrease);
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
        public static void AddDepositGoal<TMaterial>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
        {
            builder.AddGoal<DepositMaterialGoal<TMaterial>>()
                .AddCondition<AgentMaterialAmount<TMaterial>>(Comparison.SmallerThanOrEqual, 0)
                .AddCondition<PlayerMaterialAmount<TMaterial>>(Comparison.GreaterThanOrEqual, 75);
        }
        //ACTION EXTENSIONS
        public static void AddDepositAction<TMaterial, TStorage>(this GoapSetBuilder builder, int condition)
            where TMaterial : MaterialBase
            where TStorage : MaterialStorageBase
        {
            builder.AddAction<DepositMaterialAction<TMaterial, TStorage>>()
                .AddCondition<AgentMaterialAmount<TMaterial>>(Comparison.GreaterThan, condition)
                .SetTarget<ClosestStorageWithSpace<TMaterial>>()
                .AddEffect<AgentMaterialAmount<TMaterial>>(EffectType.Decrease)
                .AddEffect<PlayerMaterialAmount<TMaterial>>(EffectType.Increase);
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
        public static void AddPlayerMaterialAmountSensor<TMaterial>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
        {
            builder.AddWorldSensor<PlayerMaterialAmountSensor<TMaterial>>()
                .SetKey<PlayerMaterialAmount<TMaterial>>();
        }
    }
}