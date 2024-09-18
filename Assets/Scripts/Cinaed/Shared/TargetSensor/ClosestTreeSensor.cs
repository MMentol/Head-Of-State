using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GridMap.Resources;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.TargetSensors
{
    public class ClosestTreeSensor : LocalTargetSensorBase
    {
        private TreeResource[] trees;
        public override void Created()
        {
            //Debug.Log("Getting tree list.");
            this.trees = GameObject.FindObjectsOfType<TreeResource>();
        }

        public override void Update() {}

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            //Debug.Log($"Sense Closest Tree {agent.gameObject.name}");
            TreeResource closest = this.trees
                .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
                .FirstOrDefault();

            while (closest.rawMaterialAmount == 0)
            {
                var list = this.trees.ToList();
                list.Remove(closest);
                trees = list.ToArray();
                closest = this.trees
                .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
                .FirstOrDefault();
            }

            return new TransformTarget(closest.transform);
        }

    }
}
