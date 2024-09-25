using UnityEngine;

namespace GridMap.Resources
{
    public interface IResourceSource
    {
        public void RemoveOccupied();
        public void SetOccupied(GameObject occupant);
        public GameObject GetOccupied();
        public bool ToDestroy();
        public int GetRawMaterialAmount();
        public void Awake();
        public int Harvest(int amount);
        public bool DestroyEmpty();
    }
}