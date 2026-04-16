using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    /// <summary>
    /// ScriptableObject that defines a crafting recipe
    /// Contains input ingredients and output items
    /// </summary>
    [CreateAssetMenu(menuName = "Crafting/Recipe")]
    public class CraftingRecipeSO : ScriptableObject
    {
        [SerializeField]
        private string recipeName;

        [SerializeField]
        private string recipeDescription;

        [SerializeField]
        private List<CraftingRecipeIngredient> ingredients = new List<CraftingRecipeIngredient>();

        [SerializeField]
        private List<CraftingRecipeIngredient> outputs = new List<CraftingRecipeIngredient>();

        [SerializeField]
        private float craftingTime = 1f;

        [SerializeField]
        private Sprite recipeIcon;

        public string RecipeName => recipeName;
        public string RecipeDescription => recipeDescription;
        public List<CraftingRecipeIngredient> Ingredients => ingredients;
        public List<CraftingRecipeIngredient> Outputs => outputs;
        public float CraftingTime => craftingTime;
        public Sprite RecipeIcon => recipeIcon;

        /// <summary>
        /// Check if this recipe can be crafted with current inventory
        /// </summary>
        public bool CanCraft(Dictionary<int, InventoryItem> inventoryState)
        {
            foreach (var ingredient in ingredients)
            {
                int totalQuantity = 0;

                foreach (var inventorySlot in inventoryState)
                {
                    if (inventorySlot.Value.item.ID == ingredient.Item.ID)
                    {
                        totalQuantity += inventorySlot.Value.quantity;
                    }
                }

                if (totalQuantity < ingredient.Quantity)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Get required ingredients that are missing for this recipe
        /// </summary>
        public List<CraftingRecipeIngredient> GetMissingIngredients(Dictionary<int, InventoryItem> inventoryState)
        {
            var missingIngredients = new List<CraftingRecipeIngredient>();

            foreach (var ingredient in ingredients)
            {
                int totalQuantity = 0;

                foreach (var inventorySlot in inventoryState)
                {
                    if (inventorySlot.Value.item.ID == ingredient.Item.ID)
                    {
                        totalQuantity += inventorySlot.Value.quantity;
                    }
                }

                if (totalQuantity < ingredient.Quantity)
                {
                    missingIngredients.Add(new CraftingRecipeIngredient(
                        ingredient.Item,
                        ingredient.Quantity - totalQuantity
                    ));
                }
            }

            return missingIngredients;
        }
    }
}
