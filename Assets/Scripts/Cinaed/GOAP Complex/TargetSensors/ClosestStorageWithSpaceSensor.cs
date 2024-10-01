using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GridMap.Structures.Storage;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Complex.TargetSensors
{
    public class ClosestStorageWithSpaceSensor<TStorage> : LocalTargetSensorBase
        where TStorage : MaterialStorageBase
    {
        public TStorage[] storages;
        public override void Created() { }

        public override void Update() { }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            this.storages = MaterialDataStorage.Instance.GetStoragesOfType<TStorage>();
            var closest = this.storages
            .Where(x => x.Capacity > x.Count)
            .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
            .FirstOrDefault();

            if (closest == null)
                return null;
            else
                while (closest.Capacity <= closest.Count)
                {
                    var list = this.storages.ToList();
                    list.Remove(closest);
                    storages = list.ToArray();
                    closest = this.storages
                    .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
                    .FirstOrDefault();
                    if (closest == null)
                        return null;
                }

            Debug.Log(closest.gameObject.name);
            return new TransformTarget(closest.transform);
        }

    }
}