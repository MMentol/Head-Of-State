using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GridMap.Resources;
using System.Linq;
using UnityEngine;

namespace Cinaed.GOAP.Complex.TargetSensors
{
    public class ClosestMaterialSourceSensor<TSource> : LocalTargetSensorBase
        where TSource : ResourceSourceBase
    {
        public TSource[] sources;
        public override void Created()
        {}

        public override void Update()
        {
            this.sources = GameObject.FindObjectsOfType<TSource>();
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closest = this.sources
            .Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
            .FirstOrDefault();

            if (closest == null)
                return null;
            else
                while (closest.GetRawMaterialAmount() == 0 || closest.ToDestroy() || closest.GetOccupied() != null)
                {
                    var list = this.sources.ToList();
                    list.Remove(closest);
                    sources = list.ToArray();
                    closest = this.sources
                    .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
                    .FirstOrDefault();
                    if (closest == null)
                        return null;
                }
            return new TransformTarget(closest.transform);
        }

    }
}