using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Scripts;

namespace Cinaed.GOAP.Simple.WorldSensors
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

            return inventory.GetResourceCount("Wood") / inventory.GetRemainingCapacity();
        }
    }
}