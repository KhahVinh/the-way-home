using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(
        fileName = "SO_ItemParameter",
        menuName = "Game/Inventory/ItemParameterSO"
    )]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField]
        [Tooltip("Tên của thuộc tính cần hiển thị vd: Độ bền")]
        public string ParameterName { get; private set; }
    }
}