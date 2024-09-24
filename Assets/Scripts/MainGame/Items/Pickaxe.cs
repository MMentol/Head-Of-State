using Demos.Complex.Behaviours;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Items
{
    public class Pickaxe : ItemBase
    {
        public Pickaxe()
        {
            this.ItemName = "Pickaxe";
            this.durability = 10;
            this.craftingRecipe = new Dictionary<string, int>
            {
                {"wood", 2}, {"stone", 2}
            };
        }
        public override ItemBase CreateNewInstance()
        {
            return new Pickaxe
            {
                ItemName = "Pickaxe",
                durability = 10,
                craftingRecipe = new Dictionary<string, int>
                {
                    {"wood", 2}, {"stone", 2}
                }
            };
        }
    } 
}

