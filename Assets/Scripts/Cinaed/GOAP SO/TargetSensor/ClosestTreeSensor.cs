using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GridMap.Resources;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Simple.TargetSensors
{
    public class ClosestTreeSensor : LocalTargetSensorBase
    {
        private TreeResource[] trees;
        public override void Created()
        {
            Debug.Log("Getting trees.");
            this.trees = GameObject.FindObjectsOfType<TreeResource>();
        }

        public override void Update() {}

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closest = this.trees
                .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
                .FirstOrDefault()
                .transform;
            return new TransformTarget(closest);
        }

    }
}
