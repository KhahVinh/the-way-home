using Crafting;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Crafting.UI
{
    /// <summary>
    /// UI panel for displaying crafting recipes and handling crafting interactions
    /// </summary>
    public class UICraftingPanel : MonoBehaviour
    {
        [SerializeField]
        private CraftingSystem craftingSystem;

        [SerializeField]
        private Transform recipeListContainer;

        [SerializeField]
        private GameObject recipeButtonPrefab;

        [SerializeField]
        private TextMeshProUGUI recipeNameText;

        [SerializeField]
        private TextMeshProUGUI recipeDescriptionText;

        [SerializeField]
        private Transform ingredientsContainer;

        [SerializeField]
        private GameObject ingredientPrefab;

        [SerializeField]
        private Transform outputsContainer;

        [SerializeField]
        private GameObject outputPrefab;

        [SerializeField]
        private Button craftButton;

        [SerializeField]
        private Button togglePanelButton;

        [SerializeField]
        private CanvasGroup panelCanvasGroup;

        [SerializeField]
        private TextMeshProUGUI statusText;

        private List<Button> recipeButtons = new List<Button>();
        private CraftingRecipeSO selectedRecipe;

        private void Start()
        {
            if (craftingSystem != null)
            {
                craftingSystem.OnAvailableRecipesUpdated += UpdateRecipeList;
                craftingSystem.OnCraftingStarted += HandleCraftingStarted;
                craftingSystem.OnCraftingCompleted += HandleCraftingCompleted;
                // craftingSystem.OnCraftingFailed += HandleCraftingFailed;
            }

            if (craftButton != null)
            {
                craftButton.onClick.AddListener(OnCraftButtonClicked);
            }

            if (togglePanelButton != null)
            {
                togglePanelButton.onClick.AddListener(TogglePanel);
            }

            // Initial load of all recipes
            UpdateRecipeList(craftingSystem.GetAllRecipes());

            // Hide();
        }

        private void OnDestroy()
        {
            if (craftingSystem != null)
            {
                craftingSystem.OnAvailableRecipesUpdated -= UpdateRecipeList;
                craftingSystem.OnCraftingStarted -= HandleCraftingStarted;
                craftingSystem.OnCraftingCompleted -= HandleCraftingCompleted;
                // craftingSystem.OnCraftingFailed -= HandleCraftingFailed;
            }

            if (craftButton != null)
            {
                craftButton.onClick.RemoveListener(OnCraftButtonClicked);
            }

            if (togglePanelButton != null)
            {
                togglePanelButton.onClick.RemoveListener(TogglePanel);
            }
        }

        /// <summary>
        /// Update the list of all recipes (shows all but disables non-craftable ones)
        /// </summary>
        private void UpdateRecipeList(List<CraftingRecipeSO> availableRecipes)
        {
            // Get all recipes, not just available ones
            var allRecipes = craftingSystem.GetAllRecipes();

            // Clear existing buttons
            foreach (var button in recipeButtons)
            {
                Destroy(button.gameObject);
            }
            recipeButtons.Clear();

            // Create buttons for all recipes
            foreach (var recipe in allRecipes)
            {
                var buttonGO = Instantiate(recipeButtonPrefab, recipeListContainer);
                var button = buttonGO.GetComponent<Button>();
                var image = buttonGO.GetComponent<Image>();
                var textComponent = buttonGO.GetComponentInChildren<TextMeshProUGUI>();

                if (image && recipe.RecipeIcon)
                {
                    image.sprite = recipe.RecipeIcon;
                }

                if (textComponent)
                {
                    textComponent.text = recipe.RecipeName;
                }

                // Check if recipe can be crafted
                bool canCraft = recipe.CanCraft(craftingSystem.GetCurrentInventoryState());

                // Disable button if can't craft
                if (button)
                {
                    button.interactable = canCraft;

                    // Optional: Make non-craftable recipes look different
                    var colors = button.colors;
                    if (!canCraft)
                    {
                        colors.normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Grayed out
                        colors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    }
                    else
                    {
                        colors.normalColor = Color.white;
                        colors.disabledColor = Color.gray;
                    }
                    button.colors = colors;
                }

                var recipeToUse = recipe; // Capture for closure
                button.onClick.AddListener(() => SelectRecipe(recipeToUse));

                recipeButtons.Add(button);
            }

            // Auto-select first recipe if available
            if (allRecipes.Count > 0)
            {
                SelectRecipe(allRecipes[0]);
            }
            else
            {
                ClearRecipeDisplay();
            }
        }

        /// <summary>
        /// Select a recipe to display details
        /// </summary>
        private void SelectRecipe(CraftingRecipeSO recipe)
        {
            selectedRecipe = recipe;
            DisplayRecipeDetails();
        }

        /// <summary>
        /// Display details of the selected recipe
        /// </summary>
        private void DisplayRecipeDetails()
        {
            if (selectedRecipe == null)
                return;

            // Update recipe name and description
            if (recipeNameText)
            {
                recipeNameText.text = selectedRecipe.RecipeName;
            }

            if (recipeDescriptionText)
            {
                recipeDescriptionText.text = selectedRecipe.RecipeDescription;
            }

            // Display ingredients
            if (ingredientsContainer)
            {
                foreach (Transform child in ingredientsContainer)
                {
                    Destroy(child.gameObject);
                }

                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    var ingredientGO = Instantiate(ingredientPrefab, ingredientsContainer);
                    var text = ingredientGO.GetComponentInChildren<TextMeshProUGUI>();
                    var image = ingredientGO.GetComponentInChildren<Image>();

                    if (text)
                    {
                        text.text = $"{ingredient.Item.Name} x{ingredient.Quantity}";
                    }

                    if (image && ingredient.Item.ItemImage)
                    {
                        image.sprite = ingredient.Item.ItemImage;
                    }
                }
            }

            // Display outputs
            if (outputsContainer)
            {
                foreach (Transform child in outputsContainer)
                {
                    Destroy(child.gameObject);
                }

                foreach (var output in selectedRecipe.Outputs)
                {
                    var outputGO = Instantiate(outputPrefab, outputsContainer);
                    var text = outputGO.GetComponentInChildren<TextMeshProUGUI>();
                    var image = outputGO.GetComponentInChildren<Image>();

                    if (text)
                    {
                        text.text = $"{output.Item.Name} x{output.Quantity}";
                    }

                    if (image && output.Item.ItemImage)
                    {
                        image.sprite = output.Item.ItemImage;
                    }
                }
            }

            // Update craft button state
            UpdateCraftButtonState();
        }

        /// <summary>
        /// Clear recipe display
        /// </summary>
        private void ClearRecipeDisplay()
        {
            selectedRecipe = null;

            if (recipeNameText)
                recipeNameText.text = "No recipes available";

            if (recipeDescriptionText)
                recipeDescriptionText.text = "";

            if (ingredientsContainer)
            {
                foreach (Transform child in ingredientsContainer)
                {
                    Destroy(child.gameObject);
                }
            }

            if (outputsContainer)
            {
                foreach (Transform child in outputsContainer)
                {
                    Destroy(child.gameObject);
                }
            }

            if (craftButton)
            {
                craftButton.interactable = false;
            }
        }

        /// <summary>
        /// Update craft button state
        /// </summary>
        private void UpdateCraftButtonState()
        {
            if (craftButton == null || selectedRecipe == null)
                return;

            // Button is only interactable if not currently crafting AND recipe can be crafted
            bool canCraft = selectedRecipe.CanCraft(craftingSystem.GetCurrentInventoryState());
            craftButton.interactable = !craftingSystem.IsCrafting && canCraft;
        }

        /// <summary>
        /// Handle craft button click
        /// </summary>
        private void OnCraftButtonClicked()
        {
            if (selectedRecipe != null && craftingSystem != null)
            {
                craftingSystem.TryCraft(selectedRecipe);
            }
        }

        /// <summary>
        /// Handle crafting started
        /// </summary>
        private void HandleCraftingStarted(CraftingRecipeSO recipe)
        {
            if (statusText)
            {
                statusText.text = $"Crafting {recipe.RecipeName}...";
                statusText.color = Color.yellow;
            }

            UpdateCraftButtonState();
            UpdateRecipeButtonStates();
        }

        /// <summary>
        /// Handle crafting completed
        /// </summary>
        private void HandleCraftingCompleted(CraftingRecipeSO recipe)
        {
            if (statusText)
            {
                statusText.text = $"Crafted {recipe.RecipeName}!";
                statusText.color = Color.green;
            }

            UpdateCraftButtonState();
            UpdateRecipeButtonStates();
        }

        /// <summary>
        /// Update the state of recipe buttons (enable/disable based on craftability)
        /// </summary>
        private void UpdateRecipeButtonStates()
        {
            var allRecipes = craftingSystem.GetAllRecipes();
            var inventoryState = craftingSystem.GetCurrentInventoryState();

            for (int i = 0; i < recipeButtons.Count && i < allRecipes.Count; i++)
            {
                var button = recipeButtons[i];
                var recipe = allRecipes[i];

                if (button != null)
                {
                    bool canCraft = recipe.CanCraft(inventoryState);
                    button.interactable = canCraft;

                    // Update button colors
                    var colors = button.colors;
                    if (!canCraft)
                    {
                        colors.normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Grayed out
                        colors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    }
                    else
                    {
                        colors.normalColor = Color.white;
                        colors.disabledColor = Color.gray;
                    }
                    button.colors = colors;
                }
            }
        }

        /// <summary>
        /// Toggle panel visibility
        /// </summary>
        public void TogglePanel()
        {
            if (panelCanvasGroup.alpha > 0)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        /// <summary>
        /// Show the crafting panel
        /// </summary>
        public void Show()
        {
            if (panelCanvasGroup)
            {
                panelCanvasGroup.alpha = 1;
                panelCanvasGroup.blocksRaycasts = true;
            }
        }

        /// <summary>
        /// Hide the crafting panel
        /// </summary>
        public void Hide()
        {
            if (panelCanvasGroup)
            {
                panelCanvasGroup.alpha = 0;
                panelCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}
