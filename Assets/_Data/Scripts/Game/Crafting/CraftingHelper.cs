using Crafting;
using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    /// <summary>
    /// Helper class for quickly creating and managing crafting recipes programmatically
    /// </summary>
    public static class CraftingHelper
    {
        /// <summary>
        /// Create a simple recipe SO without needing to use the editor
        /// Useful for testing or dynamic recipe creation
        /// </summary>
        public static CraftingRecipeSO CreateRecipe(
            string recipeName,
            string recipeDescription,
            float craftingTime,
            List<(ItemSO item, int quantity)> ingredients,
            List<(ItemSO item, int quantity)> outputs
        )
        {
            var recipeId = "Dynamic_" + recipeName + "_" + System.DateTime.Now.Ticks;
            var recipe = ScriptableObject.CreateInstance<CraftingRecipeSO>();

            // Use reflection to set private fields
            var recipeNameField = typeof(CraftingRecipeSO).GetField("recipeName",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var recipeDescField = typeof(CraftingRecipeSO).GetField("recipeDescription",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var ingredientsField = typeof(CraftingRecipeSO).GetField("ingredients",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var outputsField = typeof(CraftingRecipeSO).GetField("outputs",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var craftingTimeField = typeof(CraftingRecipeSO).GetField("craftingTime",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            recipeNameField?.SetValue(recipe, recipeName);
            recipeDescField?.SetValue(recipe, recipeDescription);
            craftingTimeField?.SetValue(recipe, craftingTime);

            var ingredientsList = new List<CraftingRecipeIngredient>();
            foreach (var (item, qty) in ingredients)
            {
                ingredientsList.Add(new CraftingRecipeIngredient(item, qty));
            }
            ingredientsField?.SetValue(recipe, ingredientsList);

            var outputsList = new List<CraftingRecipeIngredient>();
            foreach (var (item, qty) in outputs)
            {
                outputsList.Add(new CraftingRecipeIngredient(item, qty));
            }
            outputsField?.SetValue(recipe, outputsList);

            recipe.name = recipeId;
            return recipe;
        }

        /// <summary>
        /// Check if player has all ingredients for a recipe
        /// </summary>
        public static bool HasAllIngredients(
            CraftingRecipeSO recipe,
            Dictionary<int, InventoryItem> inventoryState
        )
        {
            return recipe.CanCraft(inventoryState);
        }

        /// <summary>
        /// Get detailed ingredient status for UI display
        /// </summary>
        public static string GetIngredientStatus(
            CraftingRecipeIngredient ingredient,
            Dictionary<int, InventoryItem> inventoryState
        )
        {
            int totalQuantity = 0;

            foreach (var inventorySlot in inventoryState)
            {
                if (inventorySlot.Value.item.ID == ingredient.Item.ID)
                {
                    totalQuantity += inventorySlot.Value.quantity;
                }
            }

            return $"{ingredient.Item.Name}: {totalQuantity}/{ingredient.Quantity}";
        }
    }
}
