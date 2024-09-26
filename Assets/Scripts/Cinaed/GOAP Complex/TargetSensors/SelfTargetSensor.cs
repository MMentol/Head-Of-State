using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GridMap.Resources;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Complex.TargetSensors
{
    public class SelfTargetSensor : LocalTargetSensorBase
    {
        public override void Created()
        { }

        public override void Update()
        { }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            return new TransformTarget(agent.transform);
        }

    }
}