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
        public override void Created()
        {}

        public override void Update()
        {}

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var closest = MaterialDataStorage.Instance.GetSourceOfType<TSource>()
            .Where(x => x.GetRawMaterialAmount() != 0 && !x.ToDestroy() && x.GetOccupied() == null)
            .OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position))
            .FirstOrDefault();

            if (closest == null)
                return null;
            /*else
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
                }*/
            if (closest.GetType() == typeof(WaterResource))
            {
                //Debug.Log("found water: " +closest);
                return new TransformTarget(WalkableSource(agent, closest));

            }
            return new TransformTarget(closest.transform);
        }

        private Transform WalkableSource(IMonoAgent agent, TSource closestSource)
        {
            var pathfinding = agent.transform.GetComponent<HumanPathfinding>();

            var waterSource = closestSource.transform.GetComponent<Tile>();
            var neighbours = pathfinding.GetNeighbourList(waterSource);

            foreach (var neighbour in neighbours)
            {
                //Debug.Log("neibour:" + neighbour + " "+ (neighbour.isWalkable));
                if (neighbour.isWalkable)
                {
                    
                    return neighbour.transform;

                };
            }
            return waterSource.transform;
        }

    }
}