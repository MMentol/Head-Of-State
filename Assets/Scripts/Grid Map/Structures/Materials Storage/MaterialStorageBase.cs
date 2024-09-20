using UnityEngine;
namespace GridMap.Structures.Storage
{ 
    public abstract class MaterialStorageBase : MonoBehaviour
    {
        [SerializeField] public int Capacity;
        [SerializeField] public int Count;


        public int Add(int amount)
        {
            int added = Mathf.Min(amount, Capacity - Count);
            Count += added;
            return added;
        }

        public int Withdraw(int amount)
        {
            int withdrawn = Mathf.Min(amount, Count);
            Count -= withdrawn;
            return withdrawn;
        }
    }
}
