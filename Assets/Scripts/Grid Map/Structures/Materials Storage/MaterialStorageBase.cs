using UnityEngine;
namespace GridMap.Structures.Storage
{ 
    public abstract class MaterialStorageBase : MonoBehaviour
    {
        [SerializeField] public int Capacity = 100;
        [SerializeField] public int Count;

        public void UpdateResources()
        {
            MaterialDataStorage materialDataStorage = FindObjectOfType<MaterialDataStorage>();
            materialDataStorage.TallyMaterials();
        }

        public int Add(int amount)
        {
            int added = Mathf.Min(amount, Capacity - Count);
            Count += added;
            UpdateResources();
            return added;
        }

        public int Withdraw(int amount)
        {
            int withdrawn = Mathf.Min(amount, Count);
            Count -= withdrawn;
            UpdateResources();
            return withdrawn;
        }

        private void OnDestroy()
        {
            this.Capacity = 0;
            this.Count = 0;
            FindObjectOfType<MaterialDataStorage>().TallyMaterials();
        }
    }
}
