using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    private GameObject currentEnemy;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Gán spawner cho enemy
        var respawn = currentEnemy.GetComponent<EnemyRespawn>();
        if (respawn != null)
        {
            respawn.SetSpawner(this);
        }
    }

    public void OnEnemyDied()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(60f, 120f)); // Thời gian respawn ngẫu nhiên
        Spawn();
    }
}
