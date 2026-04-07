using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MyBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField]
    protected Button _button;
    #endregion

    #region Methods
    protected override void Start()
    {
        base.Start();
        AddOnClickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    protected virtual void LoadButton()
    {
        if (_button != null) return;
        _button = GetComponent<Button>();
    }

    protected virtual void AddOnClickEvent()
    {
        _button.onClick.AddListener(OnClick);
    }
    #endregion

    #region Abstract method
    protected abstract void OnClick();

    #endregion
}
