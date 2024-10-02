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
        public override void Update() { }

        public override void Created() { }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closestStorage = MaterialDataStorage.Instance.GetStoragesOfType<WaterStorage>()
                .Where(x => x.Count > 0)
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();
            var closestSource = MaterialDataStorage.Instance.GetSourceOfType<WaterResource>()
                .Where(x => x.rawMaterialAmount > 0)
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();

            if (closestStorage == null) { return new TransformTarget(closestSource.transform); }
            if (closestSource == null) { return new TransformTarget(closestStorage.transform); }

            if (Vector3.Distance(agent.transform.position, closestSource.transform.position) <= Vector3.Distance(agent.transform.position, closestStorage.transform.position))
            {
                //Transform newTar = closestSource.transform;
                //if (newTar.position.x > agent.transform.position.x) newTar.position = new Vector3(-1f, 0, 0);
                //if (newTar.position.x < agent.transform.position.x) newTar.position = new Vector3(+1f, 0, 0);
                //if (newTar.position.z > agent.transform.position.z) newTar.position = new Vector3(0, 0, -1f);
                //if (newTar.position.z < agent.transform.position.z) newTar.position = new Vector3(0, 0, +1f);
                

                return new TransformTarget(closestSource.transform);
            }           
            return new TransformTarget(closestStorage.transform);
        }

    }
    public class EdibleFoodSourceSensor : LocalTargetSensorBase
    {
        public override void Update() { }

        public override void Created() { }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closestStorage = MaterialDataStorage.Instance.GetStoragesOfType<FoodStorage>()
                .Where(x => x.Count > 0)
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();
            var closestSource = MaterialDataStorage.Instance.GetSourceOfType<FoodResource>()
                .Where(x => x.rawMaterialAmount > 0)
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
        public override void Created() { }

        public override void Update() { }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closest = MaterialDataStorage.Instance.Houses
                .Where(house => house.PeopleInside.Count < house.Capacity)
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();

            if (closest == null) { Debug.Log("No resting house available");  return null; }

            return new TransformTarget(closest.transform);
        }

    }
}
