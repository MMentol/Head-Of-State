using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GridMap.Resources;
using GridMap.Structures.Storage;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Complex.TargetSensors
{
    public class DrinkableWaterSourceSensor : LocalTargetSensorBase
    {
        public WaterStorage[] storages;
        public WaterResource[] waterTiles;
        public List<GameObject> combinedList;
        public override void Created() { }

        public override void Update()
        {
            this.storages = GameObject.FindObjectsOfType<WaterStorage>()
                .Where(x => x.Count > 0)
                .ToArray();                
            this.waterTiles = GameObject.FindObjectsOfType<WaterResource>()
                .Where(x => x.rawMaterialAmount > 0)
                .ToArray();

        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {            
            var closestStorage = this.storages
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();
            var closestSource = this.waterTiles
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();

            if (closestStorage == null) { return new TransformTarget(closestSource.transform); }
            if (closestSource == null) { return new TransformTarget(closestStorage.transform); }

            if (Vector3.Distance(agent.transform.position, closestSource.transform.position) <= Vector3.Distance(agent.transform.position, closestStorage.transform.position))
                return new TransformTarget(closestSource.transform);            
            return new TransformTarget(closestStorage.transform);
        }

    }
    public class EdibleFoodSourceSensor : LocalTargetSensorBase
    {
        public FoodStorage[] storages;
        public FoodResource[] foodTiles;
        public List<GameObject> combinedList;
        public override void Created() { }

        public override void Update()
        {
            this.storages = GameObject.FindObjectsOfType<FoodStorage>()
                .Where(x => x.Count > 0)
                .ToArray();
            this.foodTiles = GameObject.FindObjectsOfType<FoodResource>()
                .Where(x => x.rawMaterialAmount > 0)
                .ToArray();

        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closestStorage = this.storages
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();
            var closestSource = this.foodTiles
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();

            if (closestStorage == null) { return new TransformTarget(closestSource.transform); }
            if (closestSource == null) { return new TransformTarget(closestStorage.transform); }

            if (Vector3.Distance(agent.transform.position, closestSource.transform.position) <= Vector3.Distance(agent.transform.position, closestStorage.transform.position))
                return new TransformTarget(closestSource.transform);
            return new TransformTarget(closestStorage.transform);
        }

    }
    public class ClosestHouseSensor : LocalTargetSensorBase
    {
        public House[] houses;
        public override void Created() { }

        public override void Update()
        {
            this.houses = GameObject.FindObjectsOfType<House>();
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closest = this.houses
                .Where(house => house.PeopleInside.Count < house.Capacity)
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();

            if (closest == null) { return null; }

            return new TransformTarget(closest.transform);
        }

    }
}
