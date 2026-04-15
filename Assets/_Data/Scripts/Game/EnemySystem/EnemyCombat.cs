using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackCooldown = 1f;
    public int damage = 10;

    private float lastAttackTime;
    private bool disabled;

    public void TryAttack(Transform target)
    {
        if (disabled) return;

        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        StartCoroutine(AttackRoutine(target));
    }

    private IEnumerator AttackRoutine(Transform target)
    {
        var anim = GetComponent<EnemyAnimation>();
        anim.TriggerAttack();
        yield return new WaitForSeconds(0.06f);
        // Gây damage (giả định player có PlayerHealth)
        target.GetComponent<PlayerHealth>()?.TakeDamage(damage);
    }

    public void Disable()
    {
        disabled = true;
    }
}
