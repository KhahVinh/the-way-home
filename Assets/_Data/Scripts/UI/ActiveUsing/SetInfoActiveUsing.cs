using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetInfoActiveUsing : MyBehaviour
{
    #region Var
    [Header("Scriptable Object")]
    [SerializeField]
    private List<ItemControlSO> _listItemControlData;

    [Header("Components GameObject")]
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private GameObject _objItemGuide;

    [SerializeField]
    private GameObject _prefabItemGuide;

    [SerializeField]
    private HorizontalOrVerticalLayoutGroup _layout;
    [SerializeField]
    private ContentSizeFitter _fitter;
    #endregion

    public void SetInfo(int index)
    {
        this.ResetUI(); //Reset UI before set value

        ItemControlSO itemControlSO = _listItemControlData[index]; // Ref data
        _icon.sprite = itemControlSO.Image;
        _name.text = itemControlSO.Name;
        _layout.enabled = false;
        _fitter.enabled = false;
        foreach (var item in itemControlSO.ListGuideText)
        {
            string[] processString = item.Split(',');
            GameObject itemGuide = Instantiate(_prefabItemGuide, _objItemGuide.transform);
            itemGuide.GetComponent<OnSetValueItemGuide>()?.SetData(processString[0].Trim(), processString[1].Trim());
        }

        // Sửa lỗi rebuild nhiều lần gameObj cha khi dùng Content Fitter Size
        Canvas.ForceUpdateCanvases();
        _layout.enabled = true;
        _fitter.enabled = true;

        LayoutRebuilder.ForceRebuildLayoutImmediate(
            _objItemGuide.GetComponent<RectTransform>()
        );
    }

    private void ResetUI()
    {
        foreach (Transform item in _objItemGuide.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
