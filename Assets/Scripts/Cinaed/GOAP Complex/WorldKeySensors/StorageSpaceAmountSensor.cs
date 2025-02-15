using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Scripts;
using UnityEngine;

namespace Cinaed.GOAP.WorldSensors
{
    public class StorageSpaceSensor<TMaterial> : LocalWorldSensorBase
        where TMaterial : MaterialBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            MaterialDataStorage inventory = MaterialDataStorage.Instance;

            if (inventory == null)
                return false;            
            return inventory.GetRemainingCapacity(typeof(TMaterial).Name);
        }
    }
}