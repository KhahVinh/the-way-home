using UnityEngine;

public class PlayerVisualEffect : MyBehaviour
{
    #region Var
    [Header("VFX")]
    [SerializeField]
    private ParticleSystem _vfxMoveEffect;
    [Header("Animation")]
    [SerializeField]
    private Animator _animator;

    private const string ANIM_MOVE_X = "MoveX";
    private const string ANIM_MOVE_Y = "MoveY";
    private const string ANIM_SPEED = "Speed";
    #endregion

    #region Func
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (_animator != null) return;
        _animator = transform.parent.GetComponent<Animator>();
    }

    public void UpdateVisual(Vector2 moveInput)
    {
        if (moveInput == Vector2.zero)
        {
            if (_vfxMoveEffect.isPlaying)
                _vfxMoveEffect.Stop();
            return;
        }

        if (!_vfxMoveEffect.isPlaying)
            _vfxMoveEffect.Play();

    }

    public void UpdateAnim(Vector2 dir, bool isMoving)
    {
        _animator.SetFloat(ANIM_MOVE_X, dir.x);
        _animator.SetFloat(ANIM_MOVE_Y, dir.y);
        _animator.SetFloat(ANIM_SPEED, isMoving ? 1f : 0f);
    }
    #endregion
}
