using System.Collections.Generic;

namespace Items
{
    public class Bucket : ItemBase
    {
        public Bucket()
        {
            this.ItemName = "Bucket";
            this.durability = 10;
            this.craftingRecipe = new Dictionary<string, int>
            {
                {"wood", 3}
            };
        }
        public override ItemBase CreateNewInstance()
        {
            return new Bucket
            {
                ItemName = "Bucket",
                durability = 10,
                craftingRecipe = new Dictionary<string, int>
                {
                    {"wood", 3}
                }
            };
        }
    }
}