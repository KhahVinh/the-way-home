using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 2f;

    private int currentIndex;
    private float waitCounter;
    private bool isWaiting;

    private EnemyMovement movement;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
    }

    public void Patrol()
    {
        if (waypoints.Length == 0) return;

        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                isWaiting = false;
                currentIndex = (currentIndex + 1) % waypoints.Length;
            }

            movement.Stop();
            return;
        }

        Vector2 target = waypoints[currentIndex].position;
        float distance = Vector2.Distance(transform.position, target);

        if (distance < 0.2f)
        {
            isWaiting = true;
            waitCounter = waitTime;
        }
        else
        {
            movement.MoveTo(target);
        }
    }

    public float patrolRadius = 5f;

    private Vector2 randomTarget;

    public void PatrolRandom()
    {
        if (Vector2.Distance(transform.position, randomTarget) < 0.5f)
        {
            randomTarget = (Vector2)transform.position + Random.insideUnitCircle * patrolRadius;
        }

        movement.MoveTo(randomTarget);
    }
}
