using System.Collections;
using UnityEngine;

namespace GridMap.Resources
{
    public class WaterResource : ResourceSourceBase
    {
        [SerializeField][Min(1)] public int maxBound = 999999999;
        public override int Harvest(int amount)
        {
            //Debug.Log($"Started Harvesting {gameObject.name}");
            
            this.rawMaterialAmount -= amount;
            
            return (rawMaterialAmount < 0) ? -rawMaterialAmount : amount;
        }
    }
}