using UnityEngine;
namespace GridMap.Structures.Storage
{
    public abstract class MaterialStorageBase : MonoBehaviour
    {
        public string ResourceType;
        public int[] CapacityTiers = { 100, 125, 250, 500 };
        public int[] WoodUpgradeCost = { 0, 15, 20, 25 };
        public int[] StoneUpgradeCost = { 0, 5, 10, 15 };
        public int[] MetalUpgradeCost = { 0, 0, 5, 10 };
        public int currentLevel = 0;
        public int Capacity = 100;
        public int Count;
        public int totalCount;

        public void UpdateResources()
        {
            MaterialDataStorage.Instance.TallyMaterials();
        }

        public bool Upgrade()
        {
            MaterialDataStorage materialDataStorage = MaterialDataStorage.Instance;
            int upgradeLevel = currentLevel + 1;
            if (materialDataStorage.CanAfford(WoodUpgradeCost[upgradeLevel], StoneUpgradeCost[upgradeLevel], MetalUpgradeCost[upgradeLevel], 0, 0))
            {
                Debug.Log("Can upgrade");
                if (materialDataStorage.DeductCosts(WoodUpgradeCost[upgradeLevel], StoneUpgradeCost[upgradeLevel], MetalUpgradeCost[upgradeLevel], 0, 0))
                {
                    //Spent resources update
                    Structure struc = GetComponent<Structure>();
                    struc._woodSpent += WoodUpgradeCost[upgradeLevel];
                    struc._stoneSpent += StoneUpgradeCost[upgradeLevel];
                    struc._metalSpent += MetalUpgradeCost[upgradeLevel];
                    currentLevel++;
                    Capacity = CapacityTiers[currentLevel];
                    UpdateResources();
                    return true;
                }
            }
            Debug.Log("Cant upgrade");
            return false;
        }
        public int Add(int amount)
        {
            int added = Mathf.Min(amount, Capacity - Count);
            Count += added;
            totalCount += added;
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
            MaterialDataStorage.Instance.DistributeRemainingCapacity(this.ResourceType, this.Count);
            this.Capacity = 0;
            this.Count = 0;
            UpdateResources();
        }
    }
}
