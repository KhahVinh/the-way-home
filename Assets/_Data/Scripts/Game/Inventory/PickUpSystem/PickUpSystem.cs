using Inventory.Model;
using UnityEngine;

/// <summary>
/// Hệ thống nhặt đồ, khi player chạm vào item sẽ gọi hàm AddItem trong InventorySO để thêm item vào kho đồ
/// </summary>
public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponentInChildren<Item>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.Quantity = reminder;
        }
    }
}
