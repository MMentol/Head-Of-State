using System.Collections;
using UnityEngine;

namespace GridMap.Resources
{
    public class TreeResource : MonoBehaviour, IResourceSource
    {
        [SerializeField][Min(1)] public int maxBound;
        [SerializeField] public int rawMaterialAmount;

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

        public int Harvest(int amount)
        {
            Debug.Log($"Started Harvesting {gameObject.name}");
            
            this.rawMaterialAmount -= amount;
            DestroyEmpty();
            
            return (rawMaterialAmount < 0) ? -rawMaterialAmount : amount;
        }

        IEnumerator DestroyNode(GameObject node)
        {
            yield return new WaitForSeconds(5);
            GetComponent<Structure>()._tile.DestroyStructure();
        }
    }
}