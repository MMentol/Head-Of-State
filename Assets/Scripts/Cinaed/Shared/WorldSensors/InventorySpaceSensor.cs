using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Scripts;

namespace Cinaed.GOAP.WorldSensors
{
    public class InventorySpaceSensor : LocalWorldSensorBase
    {
        public override void Created() { }
        public override void Update() { }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            Inventory inventory = references.GetCachedComponent<Inventory>();

            if (inventory == null)
                return false;

            return inventory.GetRemainingCapacity();
        }
    }
}