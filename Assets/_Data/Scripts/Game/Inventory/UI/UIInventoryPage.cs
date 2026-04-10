using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        #region Var
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        [SerializeField]
        private ItemTitleSelected _itemTitle;

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnSelectionRequested,
                OnItemActionRequested,
                OnStartDragging;

        public event Action<int, int> OnSwapItems;

        [SerializeField]
        private ItemActionPanel actionPanel;

        public bool IsShowing { get; private set; }

        #endregion

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
            _itemTitle.ChangeText();
        }

        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem =
                    Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = Vector3.one;
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemPointerEnter += HandleShowDescription;
                uiItem.OnItemPointerExit += HandleHideDescription;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, string name, string description)
        {
            itemDescription.SetDescription(listOfUIItems[itemIndex].GetComponent<RectTransform>(), name, description);
            // DeselectAllItems();
            // listOfUIItems[itemIndex].Select();
        }

        internal void UpdateSelection(int itemIndex, string name)
        {
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
            _itemTitle.ChangeText(name);
        }

        public void UpdateData(int itemIndex,
            Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void HandleShowDescription(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleHideDescription(UIInventoryItem inventoryItemUI)
        {
            this.itemDescription.gameObject.SetActive(false);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnSelectionRequested?.Invoke(index);
        }

        public void Show()
        {
            // gameObject.SetActive(true);
            this.IsShowing = true;
            int size = listOfUIItems.Count / 2;
            for (int i = size; i < listOfUIItems.Count; i++)
            {
                listOfUIItems[i].gameObject.SetActive(true);
            }
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription(); // Reset text of description
            DeselectAllItems();
            _itemTitle.ChangeText(); // Reset text
        }

        public void ResetDescription()
        {
            itemDescription.ResetDescription();
        }

        public void AddAction(string actionName, Action performAction)
        {
            // actionPanel.AddButon(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            // actionPanel.Toggle(true);
            // actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
            // actionPanel.Toggle(false);
        }

        public void Hide()
        {
            // actionPanel.Toggle(false);
            // gameObject.SetActive(false);
            int size = listOfUIItems.Count / 2;
            this.IsShowing = false;
            for (int i = size; i < listOfUIItems.Count; i++)
            {
                listOfUIItems[i].gameObject.SetActive(false);
            }
            ResetDraggedItem();
        }
    }
}