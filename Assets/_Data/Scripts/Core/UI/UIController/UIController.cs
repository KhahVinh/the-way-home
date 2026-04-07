using System.Collections.Generic;

public class UIController : MyBehaviour
{
    public enum UIState
    {
        PLAYING, LEVELS
    }
    private List<IActionUI> _uiItems = new List<IActionUI>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIItems();
    }

    private void LoadUIItems()
    {
        foreach (var item in GetComponentsInChildren<IActionUI>())
        {
            _uiItems.Add(item);
        }
    }

    public void ChangeState(UIState state)
    {
        foreach (var item in _uiItems)
        {
            item.Process(state);
        }
    }

    protected override void Start()
    {
        base.Start();
        ChangeState(UIState.PLAYING);
    }

}
