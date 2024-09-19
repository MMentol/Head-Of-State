using System.Collections;
using UnityEngine;

namespace GridMap.Resources
{
    public class WaterResource : MonoBehaviour, IResourceSource
    {
        [SerializeField][Min(1)] public int maxBound = 999999;
        [SerializeField] public int rawMaterialAmount;
        [SerializeField] public bool toDestroy = false;
        [SerializeField] public GameObject occupant = null;

        public void Awake()
        {
            this.rawMaterialAmount = Random.Range(1, maxBound);
        }

        public bool DestroyEmpty()
        {
            if (rawMaterialAmount == 0)
            {
                StartCoroutine(DestroyNode(this.gameObject));
                return true;
            }
            return false;
        }

        public int GetRawMaterialAmount()
        {
            return rawMaterialAmount;
        }

        public bool ToDestroy()
        {
            return toDestroy;
        }

        public int Harvest(int amount)
        {
            //Debug.Log($"Started Harvesting {gameObject.name}");
            
            this.rawMaterialAmount -= amount;
            //DestroyEmpty();
            
            return (rawMaterialAmount < 0) ? -rawMaterialAmount : amount;
        }

        public void RemoveOccupied()
        {
            this.occupant = null;
        }

        public void SetOccupied(GameObject occupant)
        {
            this.occupant = occupant;
        }

        public GameObject GetOccupied()
        {
            return occupant;
        }

        IEnumerator DestroyNode(GameObject node)
        {
            this.toDestroy = true;
            yield return new WaitForSeconds(1);
            GetComponent<Structure>()._tile.DestroyStructure();
        }
    }
}