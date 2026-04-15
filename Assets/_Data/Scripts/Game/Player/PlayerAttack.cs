using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public int damage = 20;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public float attackCooldown = 0.5f;

    [Header("Attack Point")]
    public Transform attackPoint;

    private float lastAttackTime;
    [SerializeField]
    private Animator anim;

    public void TryAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        anim.SetTrigger("Attack");
    }

    // 👉 GỌI TỪ ANIMATION EVENT
    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (var hit in hits)
        {
            hit.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
