using Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemBase : MonoBehaviour
    {
        public string ItemName;
        public int durability;
        public Dictionary<string, int> craftingRecipe;

        public virtual ItemBase CreateNewInstance()
        {
            return new ItemBase
            {
                ItemName = this.ItemName,
                durability = this.durability,
                craftingRecipe = new Dictionary<string, int>(this.craftingRecipe)
            };
        }

        public bool IsCraftable(Inventory inventory)
        {
            Dictionary<string, int> contents = inventory.inventoryContents;
            foreach (var item in this.craftingRecipe)
            {
                string material = item.Key;
                if (contents[material] < craftingRecipe[material])
                    return false;
            }

            return true;
        }

        public bool CraftItem(Inventory inventory)
        {
            if (!IsCraftable(inventory))
                return false;

            foreach (var item in this.craftingRecipe)
            {
                string material = item.Key;
                inventory.GetFromInventory(material, craftingRecipe[material]);
            }

            ItemBase craftedItem = CreateNewInstance();
            Debug.Log(craftedItem.ItemName);
            inventory.items.Add(craftedItem);

            return true;
        }
    }
}
