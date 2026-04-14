using UnityEngine;
using UnityEngine.UI;

public class HealthItemUI : MonoBehaviour
{
    [SerializeField]
    private Image _imgHealth;
    [SerializeField]
    private CharacterHealthSO _healthData;

    private void Start()
    {
        _imgHealth.fillAmount = 1;
    }

    public void HandleHealthChange()
    {
        if (_healthData == null)
        {
            Debug.LogWarning("HealthSystem data is not assigned in HealthItemUI.");
            return;
        }
        float healthPercentage = (float)_healthData.CurrentHealth / _healthData.MaxHealth;
        _imgHealth.fillAmount = healthPercentage;
    }

}
