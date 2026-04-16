using Crafting;
using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    /// <summary>
    /// Main system managing crafting operations
    /// Handles recipe validation, item consumption, and item creation
    /// </summary>
    public class CraftingSystem : MonoBehaviour
    {
        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private List<CraftingRecipeSO> availableRecipes = new List<CraftingRecipeSO>();

        [SerializeField]
        private AudioClip craftingStartClip;

        [SerializeField]
        private AudioClip craftingCompleteClip;

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        // References for dropping items when inventory is full
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private GameObject itemPrefab;

        // Events for UI updates
        public event Action<CraftingRecipeSO> OnCraftingStarted;
        public event Action<CraftingRecipeSO> OnCraftingCompleted;
        public event Action<string> OnCraftingFailed;
        public event Action<List<CraftingRecipeSO>> OnAvailableRecipesUpdated;

        /// <summary>
        /// Check if the crafting system is currently crafting an item
        /// </summary>
        public bool IsCrafting => isCrafting;

        private bool isCrafting = false;

        private void Start()
        {
            if (inventoryData != null)
            {
                inventoryData.OnInventoryUpdated += UpdateAvailableRecipes;
            }

            UpdateAvailableRecipes(inventoryData.GetCurrentInventoryState());
        }

        private void OnDestroy()
        {
            if (inventoryData != null)
            {
                inventoryData.OnInventoryUpdated -= UpdateAvailableRecipes;
            }
        }

        /// <summary>
        /// Add a recipe to available recipes
        /// </summary>
        public void RegisterRecipe(CraftingRecipeSO recipe)
        {
            if (!availableRecipes.Contains(recipe))
            {
                availableRecipes.Add(recipe);
            }
        }

        /// <summary>
        /// Get all available recipes
        /// </summary>
        public List<CraftingRecipeSO> GetAvailableRecipes()
        {
            var validRecipes = new List<CraftingRecipeSO>();
            var inventoryState = inventoryData.GetCurrentInventoryState();

            foreach (var recipe in availableRecipes)
            {
                if (recipe.CanCraft(inventoryState))
                {
                    validRecipes.Add(recipe);
                }
            }

            return validRecipes;
        }

        /// <summary>
        /// Get all registered recipes (both craftable and non-craftable)
        /// </summary>
        public List<CraftingRecipeSO> GetAllRecipes()
        {
            return new List<CraftingRecipeSO>(availableRecipes);
        }

        /// <summary>
        /// Get current inventory state for UI checks
        /// </summary>
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            return inventoryData.GetCurrentInventoryState();
        }

        /// <summary>
        /// Attempt to craft an item using a recipe
        /// </summary>
        public bool TryCraft(CraftingRecipeSO recipe)
        {
            if (isCrafting)
            {
                OnCraftingFailed?.Invoke("Crafting is already in progress");
                return false;
            }

            var inventoryState = inventoryData.GetCurrentInventoryState();

            if (!recipe.CanCraft(inventoryState))
            {
                var missingItems = recipe.GetMissingIngredients(inventoryState);
                string missingText = "Missing: ";
                foreach (var item in missingItems)
                {
                    missingText += $"{item.Item.Name} x{item.Quantity}, ";
                }
                OnCraftingFailed?.Invoke(missingText);
                return false;
            }

            StartCoroutine(CraftCoroutine(recipe));
            return true;
        }

        /// <summary>
        /// Coroutine for crafting with delay
        /// </summary>
        private IEnumerator CraftCoroutine(CraftingRecipeSO recipe)
        {
            isCrafting = true;

            // Play crafting start sound
            if (audioSource && craftingStartClip)
            {
                audioSource.PlayOneShot(craftingStartClip);
            }

            OnCraftingStarted?.Invoke(recipe);

            // Wait for crafting time
            yield return new WaitForSeconds(recipe.CraftingTime);

            // Remove ingredients from inventory
            foreach (var ingredient in recipe.Ingredients)
            {
                RemoveItemFromInventory(ingredient.Item, ingredient.Quantity);
            }

            // Add output items to inventory or drop them
            foreach (var output in recipe.Outputs)
            {
                int remainingQuantity = output.Quantity;

                // Try to add to inventory first
                if (remainingQuantity > 0)
                {
                    int notAdded = inventoryData.AddItem(output.Item, remainingQuantity);
                    remainingQuantity = notAdded;
                }

                // If inventory is full, drop remaining items near player
                for (int i = 0; i < remainingQuantity; i++)
                {
                    DropItemNearPlayer(output.Item, 1);
                }
            }

            // Play crafting complete sound
            if (audioSource && craftingCompleteClip)
            {
                audioSource.PlayOneShot(craftingCompleteClip);
            }

            OnCraftingCompleted?.Invoke(recipe);

            isCrafting = false;
        }

        /// <summary>
        /// Remove a specific quantity of an item from inventory
        /// </summary>
        private void RemoveItemFromInventory(ItemSO item, int quantity)
        {
            int remainingToRemove = quantity;
            var inventoryState = inventoryData.GetCurrentInventoryState();

            foreach (var slot in inventoryState)
            {
                if (slot.Value.item.ID == item.ID)
                {
                    int removeAmount = Mathf.Min(remainingToRemove, slot.Value.quantity);
                    inventoryData.RemoveItem(slot.Key, removeAmount);
                    remainingToRemove -= removeAmount;

                    if (remainingToRemove <= 0)
                        break;
                }
            }
        }

        /// <summary>
        /// Update available recipes when inventory changes
        /// </summary>
        private void UpdateAvailableRecipes(Dictionary<int, InventoryItem> inventoryState)
        {
            OnAvailableRecipesUpdated?.Invoke(GetAvailableRecipes());
        }

        /// <summary>
        /// Drop an item near the player when inventory is full
        /// </summary>
        private void DropItemNearPlayer(ItemSO item, int quantity)
        {
            if (itemPrefab == null || playerTransform == null)
            {
                Debug.LogWarning("Cannot drop item: itemPrefab or playerTransform not assigned");
                return;
            }

            // Create item game object
            GameObject itemGO = Instantiate(itemPrefab, GetDropPosition(), Quaternion.identity);

            // Set item data
            var itemComponent = itemGO.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.InventoryItem = item;
                itemComponent.Quantity = quantity;
            }

            // Play drop sound
            if (audioSource && dropClip)
            {
                audioSource.PlayOneShot(dropClip);
            }

            Debug.Log($"Dropped {item.Name} x{quantity} near player (inventory full)");
        }

        /// <summary>
        /// Get a random position near the player to drop the item
        /// </summary>
        private Vector3 GetDropPosition()
        {
            if (playerTransform == null)
                return Vector3.zero;

            // Drop in a small radius around player
            float radius = 1.5f;
            Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * radius;
            Vector3 dropPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y, 0);

            return dropPosition;
        }
    }
}
