using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

namespace Cinaed.GOAP.Complex.WorldKeySensors
{
    public class PlayerMaterialAmountSensor<TMaterial> : LocalWorldSensorBase
        where TMaterial : MaterialBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            MaterialDataStorage inventory = MaterialDataStorage.Instance;

            if (inventory == null)
                return false;

            string materialType = typeof(TMaterial).Name;
            int amount = inventory.GetAmount(materialType);
            return amount;
        }
    }
}