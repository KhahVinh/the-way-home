using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    private EnemySpawner spawner;

    public void SetSpawner(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    public void NotifyDeath()
    {
        spawner?.OnEnemyDied();
    }
}
