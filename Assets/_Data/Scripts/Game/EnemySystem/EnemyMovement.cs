using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 target)
    {
        direction = (target - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;

        UpdateDirection(direction);
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    void UpdateDirection(Vector2 dir)
    {
        var anim = GetComponent<EnemyAnimation>();
        anim.SetDirection(dir);
    }
}
