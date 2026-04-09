using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }

    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        [Tooltip("Đầu vào thể hiện các giá trị hiển thị. Vd như: Độ bền các thứ")]
        public ItemParameterSO itemParameter;

        [Tooltip("Giá trị mặc định của vật phẩm khi ở trạng thái khởi tạo")]
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}

