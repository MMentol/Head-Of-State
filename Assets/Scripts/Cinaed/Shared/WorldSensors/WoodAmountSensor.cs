using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Scripts;
using UnityEngine;

namespace Cinaed.GOAP.WorldSensors
{
    public class WoodAmountSensor : LocalWorldSensorBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            Inventory inventory = references.GetCachedComponent<Inventory>();

            if (inventory == null)
                return false;

            float percentage = (float) inventory.GetResourceCount("wood") / (float) inventory.size * 100f;
            return Mathf.RoundToInt(percentage) ;
        }
    }
}