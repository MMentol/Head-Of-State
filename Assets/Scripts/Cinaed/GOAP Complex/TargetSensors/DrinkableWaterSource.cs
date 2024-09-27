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
            this.storages = (WaterStorage[]) GameObject.FindObjectsOfType<WaterStorage>()
                .Where(x => x.Count > 0);
            this.waterTiles = (WaterResource[]) GameObject.FindObjectsOfType<WaterResource>()
                .Where(x => x.rawMaterialAmount > 0);
            combinedList.Clear();
            combinedList.AddRange(this.storages.Cast<GameObject>());
            combinedList.AddRange(this.waterTiles.Cast<GameObject>());

        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closest = this.combinedList
                .OrderBy(x => Vector3.Distance(agent.transform.position, x.transform.position))
                .FirstOrDefault();
            return new TransformTarget(closest.transform);
        }

    }
}
