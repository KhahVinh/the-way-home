using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IHealth
{
    [SerializeField]
    private CharacterHealthSO _characterHealthData;
    protected Action _onHealthChange;
    [SerializeField]
    protected HealthItemUI _healthItemUI;

    protected virtual void Start()
    {
        this.InitHeal();
    }
    public void InitHeal()
    {
        _characterHealthData.CurrentHealth = _characterHealthData.MaxHealth;
        this.RegisterEventOnHealthChange();
    }

    public virtual void TakeDamage(float damage)
    {
        _characterHealthData.CurrentHealth = Mathf.Clamp(_characterHealthData.CurrentHealth - (int)damage, 0, _characterHealthData.MaxHealth);
    }

    /// <summary>
    /// Đăng ký sự kiện thay đổi máu để cập nhật UI
    /// </summary>
    protected virtual void RegisterEventOnHealthChange()
    {
        _onHealthChange += _healthItemUI.HandleHealthChange;
    }
}
