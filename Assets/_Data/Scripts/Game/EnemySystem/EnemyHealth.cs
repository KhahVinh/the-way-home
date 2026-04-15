using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    private EnemyBrain _brain;

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
        _brain.Die();
        Destroy(gameObject, 2f); // delay cho animation chết
    }
}
