using Crafting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Crafting.UI
{
    /// <summary>
    /// Display a single ingredient in the crafting UI
    /// </summary>
    public class UIIngredientDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image ingredientImage;

        [SerializeField]
        private TextMeshProUGUI quantityText;

        [SerializeField]
        private TextMeshProUGUI itemNameText;

        /// <summary>
        /// Set ingredient display data
        /// </summary>
        public void SetData(CraftingRecipeIngredient ingredient)
        {
            if (ingredient == null)
                return;

            if (ingredientImage && ingredient.Item.ItemImage)
            {
                ingredientImage.sprite = ingredient.Item.ItemImage;
            }

            if (quantityText)
            {
                quantityText.text = $"x{ingredient.Quantity}";
            }

            if (itemNameText)
            {
                itemNameText.text = ingredient.Item.Name;
            }
        }
    }
}
