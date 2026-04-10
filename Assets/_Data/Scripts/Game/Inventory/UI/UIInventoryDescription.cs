using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text title;
        [SerializeField]
        private TMP_Text description;

        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private Vector2 _offset = new Vector2(0f, 80f);

        public void Awake()
        {
            ResetDescription();
            _canvas = transform.root.GetComponent<Canvas>();
        }

        public void ResetDescription()
        {
            title.text = "";
            description.text = "";
        }

        public void SetDescription(RectTransform itemRect, string itemName, string itemDescription)
        {
            MatchUI.HandleMatchUI(_rectTransform, itemRect, _canvas, _offset);
            title.text = itemName;
            description.text = itemDescription;
            gameObject.SetActive(true);
        }
    }
}