using Inventory;
using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO weapon;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    public EquippableItemSO Weapon { get { return weapon; } }

    [SerializeField]
    private InventoryController _inventoryController;

    private void Start()
    {
        _inventoryController.OnSetDeselection += HandleDeselection;
    }

    public void HandleDeselection()
    {
        if (weapon != null)
        {
            weapon = null;
            itemCurrentState.Clear();
        }
    }
    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }

    /// <summary>
    /// Hàm này trả lại giá trị của một tham số cụ thể dựa trên itemCurentState. Nếu tham số không tồn tại trong ItemCurrentState, nó sẽ trả về 0. Hàm này hữu ích để truy cập các giá trị tham số đã được điều chỉnh bởi vũ khí hiện tại.
    /// </summary>
    /// <param name="itemParameter"></param>
    /// <returns></returns>
    public float GetValueOfParameter(ItemParameter itemParameter)
    {
        if (itemCurrentState.Contains(itemParameter))
        {
            return itemCurrentState[itemCurrentState.IndexOf(itemParameter)].value;
        }
        return 0f;
    }
}
