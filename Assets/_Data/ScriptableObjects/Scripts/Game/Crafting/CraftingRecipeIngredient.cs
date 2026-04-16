using Inventory.Model;
using System;
using UnityEngine;

namespace Crafting
{
    /// <summary>
    /// Represents an ingredient needed for a crafting recipe
    /// </summary>
    [Serializable]
    public class CraftingRecipeIngredient
    {
        [SerializeField]
        private ItemSO item;

        [SerializeField]
        private int quantity;

        public ItemSO Item => item;
        public int Quantity => quantity;

        public CraftingRecipeIngredient(ItemSO item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}
