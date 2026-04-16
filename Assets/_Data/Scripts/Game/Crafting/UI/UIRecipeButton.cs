using Crafting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory.Model;

namespace Crafting.UI
{
    /// <summary>
    /// Display a single recipe as a button in the recipe list
    /// </summary>
    public class UIRecipeButton : MonoBehaviour
    {
        [SerializeField]
        private Image recipeIcon;

        [SerializeField]
        private TextMeshProUGUI recipeNameText;

        [SerializeField]
        private Image selectedBorder;

        [SerializeField]
        private Button selectButton;

        [SerializeField]
        private InventorySO inventoryData;

        private CraftingRecipeSO recipe;

        private void Start()
        {
            if (selectButton)
            {
                selectButton.onClick.AddListener(OnClicked);
            }
        }

        private void OnDestroy()
        {
            if (selectButton)
            {
                selectButton.onClick.RemoveListener(OnClicked);
            }
        }

        /// <summary>
        /// Set recipe data
        /// </summary>
        public void SetRecipe(CraftingRecipeSO recipe)
        {
            this.recipe = recipe;

            if (recipe == null)
                return;

            if (recipeIcon && recipe.RecipeIcon)
            {
                recipeIcon.sprite = recipe.RecipeIcon;
            }

            if (recipeNameText)
            {
                recipeNameText.text = recipe.RecipeName;
            }

            UpdateCraftableState();
        }

        /// <summary>
        /// Update visual state based on whether recipe can be crafted
        /// </summary>
        public void UpdateCraftableState()
        {
            if (recipe == null || inventoryData == null)
                return;

            bool canCraft = recipe.CanCraft(inventoryData.GetCurrentInventoryState());

            // Make button look disabled if can't craft
            if (selectButton)
            {
                selectButton.interactable = canCraft;
            }
        }

        /// <summary>
        /// Set whether this button is selected
        /// </summary>
        public void SetSelected(bool selected)
        {
            if (selectedBorder)
            {
                selectedBorder.enabled = selected;
            }
        }

        /// <summary>
        /// Handle click
        /// </summary>
        private void OnClicked()
        {
            OnRecipeSelected?.Invoke(recipe);
        }

        /// <summary>
        /// Event when recipe is selected
        /// </summary>
        public event System.Action<CraftingRecipeSO> OnRecipeSelected;
    }
}
