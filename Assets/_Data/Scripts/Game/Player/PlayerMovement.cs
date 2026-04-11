using UnityEngine;

public class PlayerMovement : MyBehaviour
{
    #region Var
    [SerializeField]
    private Rigidbody2D _rb2D;
    private Vector2 _moveInput;

    [Header("Scriptable Object")]
    [SerializeField]
    private PlayerMoveSO _moveData;

    [Header("Components")]
    [SerializeField]
    private PlayerVisualEffect _playerVisualEffect;
    private Vector2 _lastMoveDir;

    public Vector2 LastMoveDir => _lastMoveDir;
    #endregion

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidBody2D();
    }

    protected virtual void LoadRigidBody2D()
    {
        if (_rb2D != null) return;
        _rb2D = transform.parent.GetComponent<Rigidbody2D>();
#if UNITY_EDITOR
        if (_rb2D == null)
            Debug.LogError(transform.parent.name + " has no Rigidbody2D component", gameObject);
#endif
    }
    #endregion

    private void Update()
    {
        // Nhận input WASD
        _moveInput.x = Input.GetAxisRaw("Horizontal"); // A - D
        _moveInput.y = Input.GetAxisRaw("Vertical");   // S - W
        _moveInput = _moveInput.normalized;

    }

    private void FixedUpdate()
    {
        // Di chuyển
        _rb2D.velocity = _moveInput * _moveData.MoveSpeed;
        _playerVisualEffect.UpdateVisual(_moveInput);
        bool isMoving = _rb2D.velocity.magnitude > 0.01f;
        if (isMoving)
        {
            _lastMoveDir = _moveInput;
        }
        _playerVisualEffect.UpdateAnim(_lastMoveDir, isMoving);
    }
}
