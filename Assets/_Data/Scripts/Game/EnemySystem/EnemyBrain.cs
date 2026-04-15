using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform player;

    [Header("Range")]
    public float detectRange = 5f;
    public float attackRange = 1.5f;

    private EnemyMovement movement;
    private EnemyCombat combat;
    private EnemyAnimation anim;
    private EnemyPatrol patrol;
    private bool isDead;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        combat = GetComponent<EnemyCombat>();
        anim = GetComponent<EnemyAnimation>();
        patrol = GetComponent<EnemyPatrol>();
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= detectRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    void Idle()
    {
        movement.Stop();
        anim.SetMoving(false);
    }

    void Chase()
    {
        movement.MoveTo(player.position);
        anim.SetMoving(true);
    }

    void Attack()
    {
        movement.Stop();
        combat.TryAttack(player);
        anim.SetMoving(false);
    }

    public void Die()
    {
        isDead = true;
        movement.Stop();
        anim.PlayDeath();
        combat.Disable();
    }

    void Patrol()
    {
        patrol.PatrolRandom();
        anim.SetMoving(true);
    }
}
