namespace GridMap.Resources
{
    public interface IResourceSource
    {
        public void Awake();
        public int Harvest(int amount);
        public bool DestroyEmpty();
    }
}