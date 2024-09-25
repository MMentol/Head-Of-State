using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

namespace Cinaed.GOAP.Complex.WorldKeySensors
{
    public class PlayerMaterialPercentageSensor<TMaterial> : LocalWorldSensorBase
        where TMaterial : MaterialBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            MaterialDataStorage inventory = GameObject.FindObjectOfType<MaterialDataStorage>();

            if (inventory == null)
                return false;
            string materialType = typeof(TMaterial).Name;
            float percentage = (float)inventory.GetAmount(materialType) / (float)inventory.GetMaxCapacity(materialType) * 100f;
            return Mathf.Max(Mathf.RoundToInt(percentage), 0);
        }
    }
}