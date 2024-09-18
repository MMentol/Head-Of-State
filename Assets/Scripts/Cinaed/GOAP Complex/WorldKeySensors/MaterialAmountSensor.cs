using Cinaed.GOAP.Complex.Interfaces;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Scripts;
using UnityEngine;

namespace Cinaed.GOAP.Complex.WorldKeySensors
{
    public class MaterialAmountSensor<TMaterial> : LocalWorldSensorBase
        where TMaterial : MaterialBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            Inventory inventory = references.GetCachedComponent<Inventory>();

            if (inventory == null)
                return false;
            string materialType = typeof(TMaterial).Name;
            float percentage = (float)inventory.GetResourceCount(materialType) / (float)inventory.size * 100f;
            return Mathf.RoundToInt(percentage);
        }
    }
}