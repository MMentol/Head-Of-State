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
using UnityEngine;

namespace Cinaed.GOAP.Complex.Factories.Extensions
{
    public static class BuilderExtensions
    {
        //GOAL EXTENSIONS
        public static void AddGatherGoal<TMaterial>(this GoapSetBuilder builder)
            where TMaterial : MaterialBase
            
        {
            builder.AddGoal<GatherMaterialGoal<TMaterial>>()
                .AddCondition<MaterialAmount<TMaterial>>(Comparison.GreaterThanOrEqual, 75);
        }

        //ACTION EXTENSIONS
        public static void AddGatherAction<TMaterial, TSource>(this GoapSetBuilder builder)
            where TMaterial: MaterialBase
            where TSource : IResourceSource
        {
            builder.AddAction<GatherMaterialAction<TMaterial, TSource>>()
                .AddCondition<InventorySpaceKey>(Comparison.GreaterThan, 0)
                .SetTarget<ClosestMaterialSource<TMaterial>>()
                .AddEffect<MaterialAmount<TMaterial>>(EffectType.Increase);
        }
        //TARGET SENSOR EXTENSIONS
        public static void AddClosestMaterialSourceSensor<TMaterial, TSource>(this GoapSetBuilder builder) 
            where TMaterial : MaterialBase
            where TSource : MonoBehaviour, IResourceSource
        {
            builder.AddTargetSensor<ClosestMaterialSourceSensor<TSource>>()
                .SetTarget<ClosestMaterialSource<TMaterial>>();
        }

        //WORLD SENSOR EXTENSIONS
        public static void AddMaterialAmountSensor<TMaterial>(this GoapSetBuilder builder)
        where TMaterial : MaterialBase
        {
            builder.AddWorldSensor<MaterialAmountSensor<TMaterial>>()
                .SetKey<MaterialAmount<TMaterial>>();
        }
        public static void AddInventorySpaceSensor(this GoapSetBuilder builder)
        {
            builder.AddWorldSensor<InventorySpaceSensor>()
                .SetKey<InventorySpaceKey>();
        }
    }
}