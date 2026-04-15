using UnityEngine;

public class EnemyHealth : HealthSystem
{
    private EnemyBrain _brain;

    public GameObject _prefabItemDrop;

    void Awake()
    {
        _brain = GetComponent<EnemyBrain>();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _brain.Hurt();
        base._onHealthChange?.Invoke();
        if (base._currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<EnemyRespawn>()?.NotifyDeath();
        _brain.Die();
        Instantiate(_prefabItemDrop, transform.position, Quaternion.identity); // Drop item
        Destroy(gameObject, 2f); // delay cho animation chết
    }
}
