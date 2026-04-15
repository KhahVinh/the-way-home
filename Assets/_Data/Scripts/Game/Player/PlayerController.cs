using UnityEngine;

public class PlayerController : MyBehaviour
{
    public UIController _uiController;
    public SetInfoActiveUsing _setInfo;
    [SerializeField]
    private PlayerChop _playerChop;
    [SerializeField]
    private PlayerAttack _playerAttack;
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
            if (!_agentWeapon.Weapon.Name.Equals("Sword"))
            {
                _playerChop.Chop();
                HandleKeySpace();
            }
            else
                _playerAttack.TryAttack();
        }
    }

    /// <summary>
    /// Handle animation
    /// </summary>
    private void HandleKeySpace()
    {
        if (_agentWeapon.Weapon.Name.Equals("Axe"))
        {
            _playerVisualEffect.UpdateAnimTriggerViaName("Chop", _playerMovement.LastMoveDir);
            return;
        }
        if (_agentWeapon.Weapon.Name.Equals("Pickaxe"))
        {
            _playerVisualEffect.UpdateAnimTriggerViaName("Mine", _playerMovement.LastMoveDir);
            return;
        }
    }
}
