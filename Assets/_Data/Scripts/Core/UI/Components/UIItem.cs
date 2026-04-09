using System;
using UnityEngine;

/// <summary>
/// Base class for UI item, which can be shown or hidden
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class UIItem : MyBehaviour, IActionUI
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private UIController.UIState _state;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvasGroup();
    }

    private void LoadCanvasGroup()
    {
        if (_canvasGroup == null)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public virtual void Hide()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public virtual void Show()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public virtual void Process(UIController.UIState state)
    {
        if (state == _state)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
