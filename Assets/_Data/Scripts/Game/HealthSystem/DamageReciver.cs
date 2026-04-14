using UnityEngine;

public class DamageReciver : MonoBehaviour
{
    private IHealth health;

    private void Awake()
    {
        health = GetComponentInChildren<IHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out DamageDealer dealer))
        {
            health.TakeDamage(dealer.damage);
        }
    }
}
