using System.Collections;
using UnityEngine;

namespace GridMap.Resources
{
    public class StoneResource : ResourceSourceBase
    {
        public override int Harvest(int amount)
        {
            //Debug.Log($"Started Harvesting {gameObject.name}");
            
            this.rawMaterialAmount -= amount;
            DestroyEmpty();
            
            return (rawMaterialAmount < 0) ? -rawMaterialAmount : amount;
        }
    }
}