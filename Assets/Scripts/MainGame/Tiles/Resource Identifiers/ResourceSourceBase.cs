using System.Collections;
using UnityEngine;

namespace GridMap.Resources
{
    public abstract class ResourceSourceBase : MonoBehaviour
    {
        [SerializeField][Min(1)] public int maxBound = 10000;
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

        public abstract int Harvest(int amount);

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
            this.gameObject.GetComponent<Structure>()._tile.DestroyStructure();
        }

        private void OnDestroy()
        {
            this.rawMaterialAmount = 0;
            MaterialDataStorage.Instance.DeRegisterSource(this.GetType(), this);
        }
    }
}