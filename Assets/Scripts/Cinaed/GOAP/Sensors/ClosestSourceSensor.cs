using Cinaed.GOAP.Behaviors;
using Cinaed.GOAP.Interfaces;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Demos;
using UnityEngine;

namespace Cinaed.GOAP.Sensors.Target
{
    public class ClosestSourceSensor<T> : LocalTargetSensorBase
        where T : IMaterial
    {
        private ResourceNodeBase<T>[] list;

        public override void Created()
        {
            this.list = GameObject.FindObjectsOfType<ResourceNodeBase<T>>();
        }

        public override void Update(){}

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closest = this.list.Closest(agent.transform.position);

            if (closest == null)
                return null;

            return new TransformTarget(closest.transform);
        }
    }
}
