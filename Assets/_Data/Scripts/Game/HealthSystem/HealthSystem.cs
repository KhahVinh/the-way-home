using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IHealth
{
    [SerializeField]
    protected CharacterHealthSO _characterHealthData;
    [SerializeField]
    protected int _currentHealth;
    protected Action _onHealthChange;
    [SerializeField]
    protected HealthItemUI _healthItemUI;

    public int CurrentHealth { get { return _currentHealth; } }
    public CharacterHealthSO CharacterHealthData { get { return _characterHealthData; } }

    protected virtual void Start()
    {
        this.InitHeal();
    }
    public void InitHeal()
    {
        _currentHealth = _characterHealthData.MaxHealth;
        this.RegisterEventOnHealthChange();
    }
    public virtual void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - (int)damage, 0, _characterHealthData.MaxHealth);
    }

    /// <summary>
    /// Đăng ký sự kiện thay đổi máu để cập nhật UI
    /// </summary>
    protected virtual void RegisterEventOnHealthChange()
    {
        if (_healthItemUI != null)
            _onHealthChange += _healthItemUI.HandleHealthChange;
    }
}
