
public class OnChangeStateUI : ButtonBase
{
    public UIController.UIState targetState;
    public UIController uIController;
    protected override void OnClick()
    {
        uIController.ChangeState(targetState);
    }
}
