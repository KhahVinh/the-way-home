using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryDropArea : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private UIInventoryPage inventoryPage;

        public void OnDrop(PointerEventData eventData)
        {
            if (inventoryPage == null)
                return;

            inventoryPage.HandleDropOutside();
        }
    }
}