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
    #endregion
}
