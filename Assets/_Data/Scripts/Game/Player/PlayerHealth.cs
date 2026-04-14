using UnityEngine;

public class PlayerHealth : HealthSystem
{
    [SerializeField]
    private PlayerVisualEffect _playerVisualEffect;
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _playerVisualEffect.PlayerHitEffect();

        //Bắn sự kiện thay đổi máu của player
        base._onHealthChange?.Invoke();
    }
}
