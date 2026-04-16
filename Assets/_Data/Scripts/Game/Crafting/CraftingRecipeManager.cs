using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    /// <summary>
    /// Manager for crafting recipes - loads and registers all recipes
    /// </summary>
    public class CraftingRecipeManager : MonoBehaviour
    {
        [SerializeField]
        private CraftingSystem craftingSystem;

        [SerializeField]
        private string recipesFolderPath = "Crafting/Recipes";

        [SerializeField]
        private bool autoLoadRecipes = true;

        private List<CraftingRecipeSO> loadedRecipes = new List<CraftingRecipeSO>();

        private void Start()
        {
            if (autoLoadRecipes)
            {
                LoadAllRecipes();
            }
        }

        /// <summary>
        /// Load all recipes from Resources folder
        /// </summary>
        public void LoadAllRecipes()
        {
            loadedRecipes.Clear();

            // Load all recipes from specified folder
            CraftingRecipeSO[] recipes = Resources.LoadAll<CraftingRecipeSO>(recipesFolderPath);

            foreach (var recipe in recipes)
            {
                RegisterRecipe(recipe);
            }

            Debug.Log($"Loaded {loadedRecipes.Count} crafting recipes");
        }

        /// <summary>
        /// Register a recipe with the crafting system
        /// </summary>
        public void RegisterRecipe(CraftingRecipeSO recipe)
        {
            if (recipe != null && !loadedRecipes.Contains(recipe))
            {
                loadedRecipes.Add(recipe);
                if (craftingSystem != null)
                {
                    craftingSystem.RegisterRecipe(recipe);
                }
            }
        }

        /// <summary>
        /// Get all loaded recipes
        /// </summary>
        public List<CraftingRecipeSO> GetLoadedRecipes()
        {
            return new List<CraftingRecipeSO>(loadedRecipes);
        }
    }
}
