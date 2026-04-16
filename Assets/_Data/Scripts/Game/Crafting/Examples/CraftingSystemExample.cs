using Crafting;
using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Crafting.Examples
{
    /// <summary>
    /// Example demonstrating how to use the crafting system
    /// This script shows common usage patterns
    /// </summary>
    public class CraftingSystemExample : MonoBehaviour
    {
        [SerializeField]
        private CraftingSystem craftingSystem;

        [SerializeField]
        private ItemSO woodItem;

        [SerializeField]
        private ItemSO plankItem;

        [SerializeField]
        private ItemSO ropeItem;

        [SerializeField]
        private ItemSO raftItem;

        private void Start()
        {
            // Example 1: Listen to crafting events
            if (craftingSystem != null)
            {
                craftingSystem.OnCraftingStarted += HandleCraftingStarted;
                craftingSystem.OnCraftingCompleted += HandleCraftingCompleted;
                craftingSystem.OnCraftingFailed += HandleCraftingFailed;
                craftingSystem.OnAvailableRecipesUpdated += HandleRecipesUpdated;
            }
        }

        private void OnDestroy()
        {
            if (craftingSystem != null)
            {
                craftingSystem.OnCraftingStarted -= HandleCraftingStarted;
                craftingSystem.OnCraftingCompleted -= HandleCraftingCompleted;
                craftingSystem.OnCraftingFailed -= HandleCraftingFailed;
                craftingSystem.OnAvailableRecipesUpdated -= HandleRecipesUpdated;
            }
        }

        /// <summary>
        /// Example: Get all available recipes and log them
        /// </summary>
        public void ListAvailableRecipes()
        {
            var recipes = craftingSystem.GetAvailableRecipes();
            Debug.Log($"Available recipes: {recipes.Count}");

            foreach (var recipe in recipes)
            {
                Debug.Log($"- {recipe.RecipeName}");
            }
        }

        /// <summary>
        /// Example: Try to craft a specific recipe
        /// </summary>
        public void TryCraftRecipe(CraftingRecipeSO recipe)
        {
            bool success = craftingSystem.TryCraft(recipe);

            if (success)
            {
                Debug.Log($"Started crafting: {recipe.RecipeName}");
            }
            else
            {
                Debug.Log($"Cannot craft: {recipe.RecipeName}");
            }
        }

        /// <summary>
        /// Example: Create a recipe programmatically
        /// </summary>
        public void CreateCustomRecipe()
        {
            var woodToPlanks = CraftingHelper.CreateRecipe(
                recipeName: "Wood to Planks",
                recipeDescription: "Process wood into wooden planks",
                craftingTime: 1.5f,
                ingredients: new List<(ItemSO, int)> { (woodItem, 1) },
                outputs: new List<(ItemSO, int)> { (plankItem, 2) }
            );

            craftingSystem.RegisterRecipe(woodToPlanks);
            Debug.Log("Custom recipe registered!");
        }

        /// <summary>
        /// Example: Event handler for when crafting starts
        /// </summary>
        private void HandleCraftingStarted(CraftingRecipeSO recipe)
        {
            Debug.Log($"[Crafting Started] {recipe.RecipeName} - Time: {recipe.CraftingTime}s");
            // You could play an animation, sound, or visual effect here
        }

        /// <summary>
        /// Example: Event handler for when crafting completes
        /// </summary>
        private void HandleCraftingCompleted(CraftingRecipeSO recipe)
        {
            Debug.Log($"[Crafting Completed] {recipe.RecipeName}");

            // Show particle effects or notification
            // Update UI to show new items
            // Play success sound
        }

        /// <summary>
        /// Example: Event handler for when crafting fails
        /// </summary>
        private void HandleCraftingFailed(string reason)
        {
            Debug.LogWarning($"[Crafting Failed] {reason}");

            // Show error message to player
            // Play error sound
        }

        /// <summary>
        /// Example: Event handler for when available recipes change
        /// </summary>
        private void HandleRecipesUpdated(List<CraftingRecipeSO> recipes)
        {
            Debug.Log($"Available recipes updated: {recipes.Count}");

            // Update recipe list UI without direct reference
            // Can be used to show/hide recipe buttons
        }

        /// <summary>
        /// Example: Check specific recipe requirements
        /// </summary>
        public void CheckRecipeRequirements(CraftingRecipeSO recipe)
        {
            Debug.Log($"\n=== Recipe: {recipe.RecipeName} ===");
            Debug.Log($"Description: {recipe.RecipeDescription}");
            Debug.Log($"Crafting Time: {recipe.CraftingTime}s");

            Debug.Log("\nIngredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                var status = CraftingHelper.GetIngredientStatus(ingredient,
                    craftingSystem.GetType().GetProperty("inventoryData")?.GetValue(craftingSystem) as Dictionary<int, InventoryItem>);
                Debug.Log($"  - {ingredient.Item.Name}: x{ingredient.Quantity}");
            }

            Debug.Log("\nOutputs:");
            foreach (var output in recipe.Outputs)
            {
                Debug.Log($"  - {output.Item.Name}: x{output.Quantity}");
            }
        }

        /// <summary>
        /// Example: Create all example recipes for the game tree
        /// Call this from a button or initialization script
        /// </summary>
        public void SetupExampleRecipes()
        {
            // Note: In production, these should be created via editor and saved as .asset files
            // This is just for demonstration

            Debug.Log("Setting up example recipes...");
            Debug.Log("In a real game, create recipes via:");
            Debug.Log("Right-click → Create → Crafting → Recipe");
            Debug.Log("Then place them in: Assets/Resources/Crafting/Recipes/");
        }
    }
}
