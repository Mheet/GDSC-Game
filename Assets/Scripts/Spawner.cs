using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public GameObject player;

    private float spawnTimer;
    private float spawnRate = 5f;

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnRate;
            spawnRate = Mathf.Clamp(spawnRate - 0.1f, 1f, 3f);
        }
    }

    private void SpawnEnemy()
    {
        var randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
