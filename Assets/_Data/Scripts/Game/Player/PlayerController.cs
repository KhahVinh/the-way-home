using UnityEngine;

public class PlayerController : MyBehaviour
{
    public UIController _uiController;
    public SetInfoActiveUsing _setInfo;
    [SerializeField]
    private PlayerChop _playerChop;
    [SerializeField]
    private AgentWeapon _agentWeapon;
    [SerializeField]
    private PlayerVisualEffect _playerVisualEffect;

    [SerializeField]
    private PlayerMovement _playerMovement;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // _uiController.ChangeState(UIController.UIState.LEVELS);
            _setInfo.SetInfo(0);
            _setInfo.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            // _uiController.ChangeState(UIController.UIState.LEVELS);
            _setInfo.SetInfo(1);
            _setInfo.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // _uiController.ChangeState(UIController.UIState.LEVELS);
            _setInfo.SetInfo(2);
            _setInfo.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _setInfo.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _agentWeapon.Weapon != null)
        {
            HandleKeySpace();
            _playerChop.Chop();
        }
    }

    private void HandleKeySpace()
    {
        if (_agentWeapon.Weapon.Name.Equals("Axe"))
        {
            _playerVisualEffect.UpdateAnimViaName("Chop", _playerMovement.LastMoveDir);
            return;
        }
        if (_agentWeapon.Weapon.Name.Equals("Pickaxe"))
        {
            _playerVisualEffect.UpdateAnimViaName("Mine", _playerMovement.LastMoveDir);
            return;
        }
    }
}
